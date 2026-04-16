CREATE TABLE [dbo].[Boat] (
    [BoatId]             INT          IDENTITY (1, 1) NOT NULL,
    [Model]              VARCHAR (30) NULL,
    [SailNumber]         VARCHAR (10) NOT NULL,
    [EngineInfo]         VARCHAR (20) NULL,
    [Draft]              FLOAT (53)   NULL,
    [Width]              FLOAT (53)   NULL,
    [BoatLength]         FLOAT (53)   NULL,
    [YearOfConstruction] VARCHAR (4)  NULL,
    [BoatType]           INT          NULL,
    PRIMARY KEY CLUSTERED ([BoatId] ASC),
    CHECK ([BoatType]>=(0))
);

CREATE TABLE [dbo].[Booking] (
    [BookingId]     INT          IDENTITY (1, 1) NOT NULL,
    [StartDate]     DATE         NULL,
    [EndDate]       DATE         NULL,
    [SailCompleted] BIT          NULL,
    [Destination]   VARCHAR (30) NULL,
    [MemberId]      INT          NULL,
    [BoatId]        INT          NULL,
    PRIMARY KEY CLUSTERED ([BookingId] ASC),
    FOREIGN KEY ([MemberId]) REFERENCES [dbo].[SailClubMember] ([MemberId]) ON DELETE CASCADE,
    FOREIGN KEY ([BoatId]) REFERENCES [dbo].[Boat] ([BoatId]) ON DELETE CASCADE,
    CONSTRAINT [CHK_dates] CHECK ([EndDate]>=[StartDate])
);

CREATE TABLE [dbo].[SailClubMember] (
    [MemberId]      INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (30)  NULL,
    [SurName]       VARCHAR (50)  NULL,
    [PhoneNumber]   VARCHAR (11)  NOT NULL,
    [MemberAddress] VARCHAR (50)  NULL,
    [City]          VARCHAR (30)  NULL,
    [Mail]          VARCHAR (100) NOT NULL,
    [MemberType]    INT           NULL,
    [MemberRole]    INT           NULL,
    [MemberImage]   VARCHAR (200) DEFAULT ('default.jpg') NULL,
    PRIMARY KEY CLUSTERED ([MemberId] ASC),
    UNIQUE NONCLUSTERED ([PhoneNumber] ASC),
    CONSTRAINT [Mem_Role] CHECK ([MemberRole]<=(2) AND [MemberRole]>=(0)),
    CONSTRAINT [Mem_Type] CHECK ([MemberType]<=(2) AND [MemberType]>=(0))
);

