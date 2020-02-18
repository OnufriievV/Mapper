using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamoTest.DAL.Data.Infrastructure
{
    public abstract class Disposable : IDisposable
    {
        private bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                DisposeCore();
            }
            isDisposed = true;
        }

        protected abstract void DisposeCore();

        ~Disposable()
        {
            Dispose(false);
        }

    }
}
