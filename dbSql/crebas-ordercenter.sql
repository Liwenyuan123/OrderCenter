/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     2017/10/24 14:06:55                          */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('O_CommodityInfo')
            and   type = 'U')
   drop table O_CommodityInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('O_FoodType')
            and   type = 'U')
   drop table O_FoodType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('O_OrderDetail')
            and   type = 'U')
   drop table O_OrderDetail
go

if exists (select 1
            from  sysobjects
           where  id = object_id('O_OrderMain')
            and   type = 'U')
   drop table O_OrderMain
go

if exists (select 1
            from  sysobjects
           where  id = object_id('O_Unit')
            and   type = 'U')
   drop table O_Unit
go

if exists (select 1
            from  sysobjects
           where  id = object_id('O_UserInfo')
            and   type = 'U')
   drop table O_UserInfo
go

/*==============================================================*/
/* Table: O_CommodityInfo                                       */
/*==============================================================*/
create table O_CommodityInfo (
   UID                  uniqueidentifier     not null,
   ComName              varchar(100)         null,
   Standard             varchar(100)         null,
   Unit                 varchar(20)          null,
   Price                decimal(10,2)        null,
   PriceSum             decimal(10,2)        null,
   State                int                  null,
   TypeID               int                  null,
   Remark               varchar(200)         null,
   constraint PK_O_COMMODITYINFO primary key (UID)
)
go

/*==============================================================*/
/* Table: O_FoodType                                            */
/*==============================================================*/
create table O_FoodType (
   ID                   int                  not null,
   PID                  int                  null,
   TypeName             varchar(50)          null,
   State                int                  null,
   Remark               varchar(200)         null,
   constraint PK_O_FOODTYPE primary key (ID)
)
go

/*==============================================================*/
/* Table: O_OrderDetail                                         */
/*==============================================================*/
create table O_OrderDetail (
   UID                  uniqueidentifier     not null,
   MainId               varchar(50)          null,
   CommodityId          varchar(50)          null,
   ComName              varchar(50)          null,
   Standard             varchar(50)          null,
   Unit                 varchar(50)          null,
   PricePlan            decimal(10,2)        null,
   NumPlan              float                null,
   PriceSumPlan         decimal(10,2)        null,
   NumReal              float                null,
   PriceReal            decimal(10,2)        null,
   PriceSumReal         decimal(10,2)        null,
   OrderState           int                  null,
   State                int                  null,
   constraint PK_O_ORDERDETAIL primary key (UID)
)
go

/*==============================================================*/
/* Table: O_OrderMain                                           */
/*==============================================================*/
create table O_OrderMain (
   UID                  uniqueidentifier     not null,
   OrderNum             varchar(50)          null,
   UsePersonName        varchar(50)          null,
   Phone                varchar(50)          null,
   ReceivePerson        varchar(50)          null,
   State                int                  null,
   OrderState           int                  null,
   Remark               varchar(50)          null,
   AddDate              datetime             null,
   constraint PK_O_ORDERMAIN primary key (UID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('O_OrderMain')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'O_OrderMain', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0-É¾³ý£¬1-Õý³£',
   'user', @CurrentUser, 'table', 'O_OrderMain', 'column', 'State'
go

/*==============================================================*/
/* Table: O_Unit                                                */
/*==============================================================*/
create table O_Unit (
   ID                   int                  not null,
   Name                 varchar(20)          null,
   constraint PK_O_UNIT primary key (ID)
)
go

/*==============================================================*/
/* Table: O_UserInfo                                            */
/*==============================================================*/
create table O_UserInfo (
   UID                  uniqueidentifier     not null,
   UserName             varchar(20)          null,
   PassWord             varchar(50)          null,
   Phone                varchar(11)          null,
   State                int                  null,
   Company              varchar(50)          null,
   Remark               varchar(200)         null,
   constraint PK_O_USERINFO primary key (UID)
)
go

