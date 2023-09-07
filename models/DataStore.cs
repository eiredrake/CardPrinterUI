using CardPrinterUI.interfaces;
using GuardNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPrinterUI.models
{
    public class DataStore : IDataStore, IDisposable
    {
        public DataStore(IDbConnection dbConnection)
        {
            Guard.NotNull(dbConnection, nameof(dbConnection));

            this.dbConnection = dbConnection;
            this.dbConnection.Open();
        }

        protected readonly IDbConnection dbConnection;
        private bool disposedValue;

        #region IDisposable
            protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    this.dbConnection.Close();
                }

                disposedValue = true;
            }
        }

            public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
