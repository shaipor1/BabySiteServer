CREATE DATABASE "BabySiteDB"
GO
USE "BabySiteDB"
GO
CREATE TABLE "User"(
    "UserId" INT IDENTITY(1,1) NOT NULL,
    "UserTypeId" INT NOT NULL,
    "LocationId" INT NOT NULL,
    "FirstName" NVARCHAR(255) NOT NULL,
    "LastName" NVARCHAR(255) NOT NULL,
    "PhoneNumber" NVARCHAR(255) NOT NULL,
    "Email" NVARCHAR(255) NOT NULL,
    "UserName" NVARCHAR(255) NOT NULL,
    "UserPswd" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "User" ADD CONSTRAINT "user_userid_primary" PRIMARY KEY("UserId");
CREATE TABLE "UserType"(
    "UserTypeId" INT IDENTITY(1,1) NOT NULL,
    "UserTypeName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "UserType" ADD CONSTRAINT "usertype_usertypeid_primary" PRIMARY KEY("UserTypeId");
CREATE TABLE "Massage"(
    "MassageId" INT IDENTITY(1,1) NOT NULL,
    "MassageTypeId" INT NOT NULL,
    "UserId" INT NOT NULL,
    "HeadLine" NVARCHAR(255) NOT NULL,
    "Body" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Massage" ADD CONSTRAINT "massage_massageid_primary" PRIMARY KEY("MassageId");
CREATE TABLE "MassageType"(
    "MassageTypeId" INT IDENTITY(1,1) NOT NULL,
    "UserTypeId" INT NOT NULL,
    "MassageTypeName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "MassageType" ADD CONSTRAINT "massagetype_massagetypeid_primary" PRIMARY KEY("MassageTypeId");
CREATE TABLE "BabySitter"(
    "BabySitterId" INT IDENTITY(1,1) NOT NULL,
    "UserId" INT NOT NULL,
    "RatingAverage" INT NOT NULL,
    "HasCar" BIT NOT NULL,
    "Age" INT NOT NULL,
    "Salary" INT NOT NULL
);
ALTER TABLE
    "BabySitter" ADD CONSTRAINT "babysitter_babysitterid_primary" PRIMARY KEY("BabySitterId");
CREATE TABLE "Parents"(
    "ParentId" INT IDENTITY(1,1) NOT NULL,
    "UserId" INT NOT NULL,
    "ChildrenCount" INT NOT NULL,
    "ChildrenMinAge" INT NOT NULL,
    "ChildrenMaxAge" INT NOT NULL,
    "HasDog" BIT NOT NULL
);
ALTER TABLE
    "Parents" ADD CONSTRAINT "parents_parentid_primary" PRIMARY KEY("ParentId");
CREATE TABLE "Location"(
    "LocationId" INT IDENTITY(1,1)  NOT NULL,
    "CityId" INT NOT NULL,
    "HouseId" INT NOT NULL,
    "Street" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Location" ADD CONSTRAINT "location_locationid_primary" PRIMARY KEY("LocationId");
CREATE TABLE "Request"(
    "RequestId" INT IDENTITY(1,1) NOT NULL,
    "ParentId" INT NOT NULL,
    "BabySitterId" INT NOT NULL,
    "MassageId" INT NOT NULL,
    "RequestStatusId" INT NOT NULL
);
ALTER TABLE
    "Request" ADD CONSTRAINT "request_requestid_primary" PRIMARY KEY("RequestId");
CREATE TABLE "Reviews"(
    "ReviewId" INT IDENTITY(1,1) NOT NULL,
    "ParentId" INT NOT NULL,
    "BabySitterId" INT NOT NULL,
    "Rating" INT NOT NULL,
    "Decription" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Reviews" ADD CONSTRAINT "reviews_reviewid_primary" PRIMARY KEY("ReviewId");
CREATE TABLE "Area"(
    "AreaId" INT IDENTITY(1,1) NOT NULL,
    "AreaName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Area" ADD CONSTRAINT "area_areaid_primary" PRIMARY KEY("AreaId");
CREATE TABLE "City"(
    "CityId" INT IDENTITY(1,1) NOT NULL,
    "CityName" NVARCHAR(255) NOT NULL,
    "AreaId" INT NOT NULL
);
ALTER TABLE
    "City" ADD CONSTRAINT "city_cityid_primary" PRIMARY KEY("CityId");
CREATE TABLE "RequestStatus"(
    "RequestStatusId" INT IDENTITY(1,1) NOT NULL,
    "RequestStatusName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "RequestStatus" ADD CONSTRAINT "requeststatus_requeststatusid_primary" PRIMARY KEY("RequestStatusId");
ALTER TABLE
    "BabySitter" ADD CONSTRAINT "babysitter_userid_foreign" FOREIGN KEY("UserId") REFERENCES "User"("UserId");
ALTER TABLE
    "Massage" ADD CONSTRAINT "massage_userid_foreign" FOREIGN KEY("UserId") REFERENCES "User"("UserId");
ALTER TABLE
    "Parents" ADD CONSTRAINT "parents_userid_foreign" FOREIGN KEY("UserId") REFERENCES "User"("UserId");
ALTER TABLE
    "User" ADD CONSTRAINT "user_usertypeid_foreign" FOREIGN KEY("UserTypeId") REFERENCES "UserType"("UserTypeId");
ALTER TABLE
    "MassageType" ADD CONSTRAINT "massagetype_usertypeid_foreign" FOREIGN KEY("UserTypeId") REFERENCES "UserType"("UserTypeId");
ALTER TABLE
    "Request" ADD CONSTRAINT "request_massageid_foreign" FOREIGN KEY("MassageId") REFERENCES "Massage"("MassageId");
ALTER TABLE
    "Massage" ADD CONSTRAINT "massage_massagetypeid_foreign" FOREIGN KEY("MassageTypeId") REFERENCES "MassageType"("MassageTypeId");
ALTER TABLE
    "Reviews" ADD CONSTRAINT "reviews_babysitterid_foreign" FOREIGN KEY("BabySitterId") REFERENCES "BabySitter"("BabySitterId");
ALTER TABLE
    "Request" ADD CONSTRAINT "request_babysitterid_foreign" FOREIGN KEY("BabySitterId") REFERENCES "BabySitter"("BabySitterId");
ALTER TABLE
    "Reviews" ADD CONSTRAINT "reviews_parentid_foreign" FOREIGN KEY("ParentId") REFERENCES "Parents"("ParentId");
ALTER TABLE
    "Request" ADD CONSTRAINT "request_parentid_foreign" FOREIGN KEY("ParentId") REFERENCES "Parents"("ParentId");
ALTER TABLE
    "User" ADD CONSTRAINT "user_locationid_foreign" FOREIGN KEY("LocationId") REFERENCES "Location"("LocationId");
ALTER TABLE
    "City" ADD CONSTRAINT "city_areaid_foreign" FOREIGN KEY("AreaId") REFERENCES "Area"("AreaId");
ALTER TABLE
    "Location" ADD CONSTRAINT "location_cityid_foreign" FOREIGN KEY("CityId") REFERENCES "City"("CityId");
ALTER TABLE
    "Request" ADD CONSTRAINT "request_requeststatusid_foreign" FOREIGN KEY("RequestStatusId") REFERENCES "RequestStatus"("RequestStatusId");



    
    USE [BabySiteDB]
GO

INSERT INTO [dbo].[UserType]
           (
           [UserTypeName])
     VALUES
           (
           'parent')
GO

USE [BabySiteDB]
GO

INSERT INTO [dbo].[UserType]
           (
           [UserTypeName])
     VALUES
           (
           'baby sitter')
GO

USE [BabySiteDB]
GO

INSERT INTO [dbo].[Area]
           ([AreaName])
     VALUES
           ('Hasharon')
GO


USE [BabySiteDB]
GO

INSERT INTO [dbo].[City]
           ([CityName]
           ,[AreaId])
     VALUES
           ('Hod Hashron'
           ,1)
GO


USE [BabySiteDB]
GO

INSERT INTO [dbo].[Location]
           ([CityId]
           ,[HouseId]
           ,[Street])
     VALUES
           (1
           ,18
           ,'צנחנים')
GO


USE [BabySiteDB]
GO

INSERT INTO [dbo].[User]
           ([UserTypeId]
           ,[LocationId]
           ,[FirstName]
           ,[LastName]
           ,[PhoneNumber]
           ,[Email]
           ,[UserName]
           ,[UserPswd])
     VALUES
           (1
           ,1
           ,'דניאל'
           ,'עוז'
           ,'0545887080'
           ,'danieloz@gmail.com'
           ,'danieloz'
           ,'1234')
GO




