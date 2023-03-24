CREATE DATABASE LeaderBank;
use LeaderBank;

CREATE TABLE Customers(

Customer_Id INT IDENTITY NOT NULL,
Names VARCHAR(100),
Surnames VARCHAR(100),
Address VARCHAR(200),
Email VARCHAR(100),
Phone VARCHAR(20),
Birthdate DATETIME,
Occupation VARCHAR(100),
Gender VARCHAR(30),
PRIMARY KEY (Customer_Id),

);


CREATE TABLE Accounts(

Account_Id INT IDENTITY NOT NULL,
Id_Customer INT NOT NULL,
AccountType VARCHAR(50),
Balance DECIMAL,
OpenDate DATETIME, 
CloseDate DATETIME,
ManagementCost DECIMAL NOT NULL,
AccountState VARCHAR(50),
PRIMARY KEY (Account_Id),
CONSTRAINT Fk_Id_Customer
FOREIGN KEY(Id_Customer)
REFERENCES Customers(Customer_Id)
);

CREATE TABLE Transactions(

Transaction_Id INT IDENTITY NOT NULL,
Id_Account INT NOT NULL,
TransactionDate VARCHAR(20),
TransactionHour VARCHAR(20),
TransactionType VARCHAR(100),
Description VARCHAR(250),
Amount DECIMAL,
OldBalance DECIMAL,
FinalBalance DECIMAL,
TransactionState VARCHAR(50),
PRIMARY KEY (Transaction_Id),
CONSTRAINT Fk_Id_Account
FOREIGN KEY(Id_Account)
REFERENCES Accounts(Account_Id),
);

CREATE TABLE Cards(
Card_Id INT  IDENTITY NOT NULL,
Id_Account INT NOT NULL,
NumberCard VARCHAR(20),
Cvc VARCHAR(4),
EmissionDate DATETIME,
ExpirationDate DATETIME,
CardState VARCHAR(50),
PRIMARY KEY (Card_Id),
CONSTRAINT Fk_IdAccount
FOREIGN KEY(Id_Account)
REFERENCES Accounts(Account_Id)
);