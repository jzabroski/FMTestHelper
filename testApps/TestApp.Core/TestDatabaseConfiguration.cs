using System;
using System.Data.Common;

namespace TestApp.Core
{
    public class TestDatabaseConfiguration
    {

        public TestDatabaseConfiguration(
            string adminConnectionString,
            DbProviderFactory providerFactory,
            string testDatabaseName,
            string databaseNameKey = "Initial Catalog"
        )
        {
            DbProviderFactory = providerFactory
                                ?? throw new ArgumentNullException(nameof(providerFactory));

            AdminConnectionString = adminConnectionString;
            TestDatabaseName = testDatabaseName;
            DatabaseNameKey = databaseNameKey;
        }

        public DbProviderFactory DbProviderFactory { get; }

        /// <summary>Connection string for an account which can connect
        /// and create/destroy databases.</summary>
        public string AdminConnectionString { get; }

        public string TestConnectionString
        {
            get
            {
                var testCSB = DbProviderFactory.CreateConnectionStringBuilder();

                // Use the admin connection string, update it with the test database name
                testCSB.ConnectionString = AdminConnectionString;
                testCSB[DatabaseNameKey] = TestDatabaseName;
                return testCSB.ConnectionString;
            }
        }

        /// <summary>The key used to find the database name in a
        /// ConnectionStringBuilder collection.  Some database providers
        /// use different keys.</summary>
        public string DatabaseNameKey { get; set; }
        public string TestDatabaseName { get; set; }
    }
}