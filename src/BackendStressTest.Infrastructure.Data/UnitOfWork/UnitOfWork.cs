namespace BackendStressTest.Infrastructure.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _dbSession;

        public UnitOfWork(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public void BeginTransaction()
        {
            _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbSession.Transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            _dbSession.Transaction?.Dispose();
        }

        public void Rollback()
        {
            _dbSession.Transaction?.Rollback();
            Dispose();
        }
    }
}
