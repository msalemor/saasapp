# Multi-tenant SaaS Application

This guide shows how to develop a simple multi-tenant SaaS application.
 
## Requirements

The guide is based on following technologies:

- Visual Studio 2017 or above or Visual Studio Code
- .Net Core 2.1
- MSSQL (Azure Sql)
- Self-signed certificates for development
 

## Solution

Solution approach:

- Application deployed with SNI certificates (same IP with multiple host names)
- Application determines the host based on the host name and renders content specific to tenant
- User is presetend with a login screen
- After authentication, if the user has a claim indicating that he has access to the site, the user is authorized and all database calls are made to authorized tenant

### Database model and solution design

![alt text](media/saas-multi-tenant-app-database-per-tenant-pool-15.png)

### Step 1

Create a local sql database:

```sql
create table Catalog 
(
    TenantId varchar(50) not null primary key,
    Description varchar(50) not null,
    Server varchar(50) not null,
    Database varchar(50) not null,
    Port varchar(10) not null default 1433    
)
```

### Step 2

Create a a Catalog table:

```sql
create table Expense
(
    TransactionId int not null primary key,
    Description varchar(50) not null,
    Amount money not null default 0,
    CreatedBy varchar(50) not null,
    CreatedDate datetime not null default getutcdate()
)
```

### Step 3

Generate the self-signed certificates.


### Step 4


## References

- [Multi-tenant SaaS database tenancy patterns](https://docs.microsoft.com/en-us/azure/sql-database/saas-tenancy-app-design-patterns)