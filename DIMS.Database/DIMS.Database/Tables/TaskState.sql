CREATE TABLE [dbo].[TaskState]
(
	[StateId] INT NOT NULL, 
    [StateName] VARCHAR(50) NOT NULL 

	CONSTRAINT [PK_State] PRIMARY KEY ([StateId])
)
