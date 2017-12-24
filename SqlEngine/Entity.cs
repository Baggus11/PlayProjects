using System;
using System.Data.SqlClient;
using System.Reflection;

namespace DataAccess
{
    /// <summary>
    /// Abstract base class with provides any POCO class a means of connecting and performing sql operations instantly.
    /// Some SQL will be built by dynasql.
    /// Raw SQL may be used by the child class.
    /// </summary>
    /// <typeparam name="TChild"></typeparam>
    public abstract class Entity<TChild> //Note: I want the type of the child that derives from this abstract base to drive the generation of sqlparams AND what gets searched, inserted, deleted, merged, joined, etc.
    {
        public bool MatchesSchema { get; }

        public SqlConnectionStringBuilder ConnectionStringBuilder { get => _connectionStringBuilder; set => _connectionStringBuilder = value; }

        protected SqlParameter[] TableParameters { get; } //parameters derived from sql table
        protected SqlParameter[] DerivedParameters { get; } //params generated from derived class

        //TODO: create a method that allows switching between parameter sets. 
        //TODO: create a method that creates both sets of parameters.

        private SqlConnectionStringBuilder _connectionStringBuilder; //Facilitates building and checking connection strings

        protected internal bool CheckSchema<TChild>()
        {
            throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        } //Checks TChild to see if the child class in any way matches the database specified.

        public Entity(string connectionString) //verifies connectionstring is in proper format, else throws exception.  Iff solid, 
        { }

        internal abstract bool GetSqlParams(); //Already implemeted by DAL
        internal abstract void CreateSqlParams(); //Create and store both sets of SqlParams.  Throw if either set is null and specify.

        public abstract void SwapParameterSet(ParameterCreationOption option); //Warn if one set or both are null.

        public virtual bool RowExists<T>(T item) //check that an item exists in the table
            where T : class, new() =>
                throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);

        //void RunQuery(); //Run the query built up in this object so far.  (this will be a modified version of CWAccess.SqlResult)
    }
}
