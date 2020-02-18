using CamoTest.BLL.DTO;
using CamoTest.DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CamoTest.BLL.Service
{
    public interface IFileService
    {
        MappingTableDTO GetMappingTableDTO(int requestId, string path, string extension);

        string[] GetErrorList(int requestId, int[] values, string path, string ext);

        int SaveFile(Stream stream, string path, string extension, string uploadedFileName, int temporaryFileStorageTime);

        void DeleteFile(int requestId, string path, string extension);
    }

    public class FileService : IFileService
    {
        public IRequestService RequestService { get; set; }

        public IHangFireService HangFireService { get; set; }

        public int SaveFile(Stream stream, string path, string extension, string uploadedFileName, int temporaryFileStorageTime)
        {
            CsvMemento memento = null;
            Guid newFileName;
            try
            {

                memento = new CsvMemento(stream);
                if (memento == null)
                    throw new NullReferenceException();
                newFileName = Guid.NewGuid();
                using (Stream fs = new FileStream(path + newFileName + extension, FileMode.Create, FileAccess.Write))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, memento);
                }
            }

            catch (Exception ex)
            {
                return 0;
            }

             int id = RequestService.Create(new Request()
            {
                DateCreated = DateTime.Now,
                RealFileName = newFileName,
                UploadedFileName = uploadedFileName
            }, path, extension);

            HangFireService.CreateDeletionJobForFile(id, path, extension, temporaryFileStorageTime);

            return id;
        }

        public MappingTableDTO GetMappingTableDTO(int requestId, string path, string extension)
        {
            var request = RequestService.GetByID(requestId);

            if (request == null)
                return null;

            CsvMemento memento = ReadFile(path + request.RealFileName + extension);
            if (memento == null)
                return null;

            var processor = new CsvProcessor(memento);

            var mappingTableDTO = new MappingTableDTO()
            {
                DisplayFileName = request.UploadedFileName,
                Headers = processor.Headers.ToList(),
                RequestId = request.Id,
                HeadersErrors = processor.GetHeaderErrors()
            };

            for (int i = 0; i < processor.ColumnLenght; i++)
            {
                mappingTableDTO.TableItemsDTO.Add(new MappingTableItemDTO()
                {
                    Name = processor.Headers[i],
                    ValueExamples = processor.GetValueExamples(i)
                });
            }

            return mappingTableDTO;

        }

        public string[] GetErrorList(int requestId, int[] values, string path, string ext)
        {
            var request = RequestService.GetByID(requestId);

            if (request == null)
                return null;
            var memento = ReadFile(path + request.RealFileName + ext);

            if (memento == null)
                return null;

            return new CsvProcessor(memento).GetErrors(values);

        }

        public void DeleteFile(int requestId, string path, string extension)
        {
            var request = RequestService.GetByID(requestId);

            File.Delete(path + request.RealFileName + extension);
        }

        private CsvMemento ReadFile(string filepath)
        {
            CsvMemento memento = null;
            try
            {
                using (Stream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    memento = (CsvMemento)formatter.Deserialize(fs);
                }
            }

            catch (Exception ex)
            {
                return null;
            }

            return memento;
        }
    }

    [Serializable]
    class CsvMemento
    {
        readonly string[][] _arrayStr;

        public string[][] Data => _arrayStr;

        public CsvMemento(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                _arrayStr = reader.ReadToEnd()
                    .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim('\r')
                        .Split(',')
                        .Select(str => str.Trim())
                        .ToArray())
                    .ToArray();
            }
        }
    }

    class CsvProcessor
    {
        readonly string[][] _arrayStr;

        public string[] Headers => _arrayStr[0];

        public int ColumnLenght => _arrayStr[0].Length;

        public CsvProcessor(CsvMemento csvMemento)
        {
            if (csvMemento == null)
                throw new NullReferenceException();
            _arrayStr = csvMemento.Data;
        }

        public string[] GetColumn(int columnNumber)
        {
            return Enumerable.Range(0, _arrayStr.Length).Select(x => _arrayStr[x][columnNumber]).ToArray();
        }

        public string GetValueExamples(int columnNumber)
        {
            var allUnique = Enumerable.Range(0, _arrayStr.Length).Select(x => _arrayStr[x][columnNumber]).Distinct().Skip(1);

            return allUnique.Count() > 5 ? String.Join(" / ", allUnique.Take(4).ToArray()) + " /..." : String.Join(" / ", allUnique.Take(5).ToArray());

        }

        public string GetHeaderErrors()
        {
            var strBuilder = new StringBuilder();

            strBuilder.Append(Headers.Any(i => String.IsNullOrWhiteSpace(i)) ? "Some header name is empty " : String.Empty);

            strBuilder.Append(Headers.Any(i => i.Contains(" ")) ? "Some header name contains whitespace " : String.Empty);

            var regEx = new Regex(@"^\w+$");

            strBuilder.Append(Headers.Any(i => !regEx.IsMatch(i)) ? "Some header name contains forbidden symbols " : String.Empty);

            strBuilder.Append(Headers.Count() != Headers.Distinct().Count() ? "Some header name isn'n unique " : String.Empty);

            return strBuilder.ToString();
        }

        public string[] GetErrors(int[] values)
        {
            string[] errors = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var column = GetColumn(i);
                switch (values[i])
                {
                    case 0: case 5: case 6: case 7:
                        errors[i] = (String.Empty);
                        break;


                    case 1: case 2:
                        errors[i] = column.Any(s => String.IsNullOrWhiteSpace(s)) ? "Contains empty entry(s) " : String.Empty;
                        break;

                    case 3:
                        string str = column.Any(s => String.IsNullOrWhiteSpace(s)) ? "Contains empty entry(s) " : String.Empty +
                            (column.Any(s => float.TryParse(s, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("en-US"), out float f)) ?
                            String.Empty : "Contains not numeric entry(s)");
                        errors[i] = str;
                        break;

                    case 4:
                        errors[i] = column
                            .Where(s => !String.IsNullOrWhiteSpace(s))
                            .Any(s => float.TryParse(s, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("en-US"), out float f)) ? String.Empty : "Contains not numeric entry(s)";
                        break;
                }
            }

            int posSKU = Array.IndexOf(values, 1);
            int posBrand = Array.IndexOf(values, 2);

            if (posSKU != -1 && posBrand != -1)
            {
                var arrSkuPlusBrend = GetColumn(posSKU)
                    .Zip(GetColumn(posBrand), (s1, s2) => s1 + s2);

                if(arrSkuPlusBrend.Count() != arrSkuPlusBrend.Distinct().Count())
                {
                    errors[posSKU] += "Brand + SKU contains not unique entry(s)";
                    errors[posBrand] += "Brand + SKU contains not unique entry(s)";
                }
            }

            return errors;
        }
    }

}