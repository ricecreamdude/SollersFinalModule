CREATE DATABASE finalDB;

CREATE TABLE Customer (
	Id int primary key identity(1,1),
	FirstName varchar(20),
	LastName varchar(20),
	Email varchar(50)
);

INSERT INTO Customer VALUES( 'Joshua', 'Ho', 'ho.joshua4@gmail.com');
INSERT INTO Customer VALUES( 'Anna', 'Liu', 'annaliu@hotmail.com');
INSERT INTO Customer VALUES( 'Jia Jie', 'Choong', 'jj@malaysia.com');
INSERT INTO Customer VALUES( 'Erik', 'Erikson', 'ee@azure.com');
INSERT INTO Customer VALUES( 'Patrick', 'Starr', 'underarock@bbottom.com');