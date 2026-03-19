DROP TABLE IF EXISTS Booking;
DROP TABLE IF EXISTS SailClubMember;
DROP TABLE IF EXISTS Boat;


CREATE TABLE SailClubMember(
	MemberId int identity(1,1) NOT NULL PRIMARY KEY,
    FirstName VARCHAR(30),
    SurName VARCHAR(20),
    PhoneNumber VARCHAR(11) NOT NULL UNIQUE,
    MemberAddress VARCHAR(50),
    City VARCHAR(30),
    Mail VARCHAR(100) NOT NULL,
    MemberType int CHECK (MemberType >= 0),
    MemberRole int CHECK (MemberRole >= 0), 
    MemberImage VARCHAR
);

CREATE TABLE Boat(
    BoatId int identity(1,1) NOT NULL PRIMARY KEY,
    Model VARCHAR(30),
    SailNumber VARCHAR(10) NOT NULL,
    EngineInfo VARCHAR(20),
    Draft FLOAT,
    Width FLOAT,
    BoatLength FLOAT,
    YearOfConstruction VARCHAR(4),
    BoatType int CHECK (BoatType >= 0)
);

CREATE TABLE Booking(
    BookingId int identity(1,1) NOT NULL PRIMARY KEY,
    StartDate DATE,
    EndDate DATE,
    SailCompleted BIT,
    Destination VARCHAR(30),
    MemberId int,
    BoatId int,

    FOREIGN KEY (MemberId) REFERENCES SailClubMember (MemberId) ON DELETE CASCADE,
    FOREIGN KEY (BoatId) REFERENCES Boat (BoatId) ON DELETE CASCADE,
    CONSTRAINT CHK_dates CHECK (EndDate >= StartDate)
);
