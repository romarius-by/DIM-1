CREATE TABLE [dbo].[KnowledgeAssessment]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[KnowledgeTestId] INT,
	[AverageScore] FLOAT,
	[MathScore] FLOAT,

	CONSTRAINT PK_KnowledgeAssessment PRIMARY KEY ([Id]),
	CONSTRAINT FK_KnowledgeAssessment_To_KnowledgeTest FOREIGN KEY ([KnowledgeTestId]) REFERENCES KnowledgeTest(Id) ON DELETE CASCADE
)
