


create table People(

PersonID int identity(1,1) not null,
Phone nvarchar(20) not null unique,
FullName nvarchar(50) not null

constraint PK_People_PersonID primary key (PersonID)

);


create table Users(

UserID int identity(1,1) unique,
UserName nvarchar(20) not null unique,
Password nvarchar(150) not null,
permations int not null,
IsActive bit not null default 1,
PersonID int not null unique,
Createdby int ,
CreatedAt datetime not null default getdate(),
UpdateAt date ,

constraint PK_Users_UserID Primary key (UserID),
constraint FK_Users_PersonID Foreign key (PersonID) References People(PersonID),
constraint FK_Users_Createdby Foreign key (Createdby) References Users(UserID),


);



create table Clients(

ClientID int identity(1,1) not null,
PersonID int not null unique,
Createdby int not null,

constraint PK_Clients_ClientID primary key (ClientID),
constraint FK_Clients_PersonID foreign key (PersonID) References People(PersonID),
constraint FK_Clients_UserID foreign key (Createdby) References Users(UserID)


);


create table Debts(

DebtID int identity(1,1) not null,
ClientID int not null unique,
AmountOwed decimal(10,2) not null,
Createdby int not null,
CreatedAt datetime not null default getdate(),
updateAt datetime,


constraint PK_Debts_DebtID Primary key (DebtID),
constraint FK_Debts_ClientID Foreign key (ClientID) References Clients(ClientID),
constraint FK_Debts_Createdby Foreign key (Createdby) References Users(UserID)



);

create table Prices(

PriceID int identity(1,1) not null,
BilliardPrice decimal(10,2) not null default 1.00,
PingPongPrice decimal(10,2) not null default 1.00,
PlaystationPrice decimal(10,2) not null default 1.00,
CreatedAt datetime not null default getdate(),
Createby int not null,
Updatedby int,
UpdatedAt datetime,
IsActive bit default 1,


constraint PK_Prices_PriceID Primary key (PriceID),
constraint FK_Prices_Createby foreign key (Createby) References Users(UserID),
constraint FK_Prices_Updatedby foreign key (Updatedby) References Users(UserID)

);


create table Payments(

PaymentID int identity(1,1) not null,
DebtID int not null,
Amount decimal(10,2) not null,
CreatedAt datetime not null default getdate(),
Createdby int not null,

constraint PK_Payments_PaymentID Primary key (PaymentID),
constraint FK_Payments_DebtID foreign key (DebtID) References Debts(DebtID),
constraint FK_Payments_Createdby foreign key (Createdby) References Users(UserID)



);

create table Sessions(

SessionID int identity(1,1) not null,
ClientID int not null,
StartTime datetime not null default getdate(),
EndTime datetime,
GameType nvarchar(30) not null,
TotalAmount decimal(10,2) not null,

constraint PK_Sessions_SessionID Primary key (SessionID),
constraint FK_Sessions_ClientID foreign key (ClientID) References Clients(ClientID)


);