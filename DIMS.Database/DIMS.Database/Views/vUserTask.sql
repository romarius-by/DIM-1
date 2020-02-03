CREATE VIEW [dbo].[vUserTask]
	AS SELECT ISNULL(ut.UserId, -999) AS UserId,
	ISNULL(ut.TaskId, -999) AS TaskId,
	t.Name AS TaskName,
	t.Description AS Description,
	t.StartDate AS StartDate,
	t.DeadlineDate AS DeadlineDate,
	ts.StateName AS State
	FROM [UserTask] ut JOIN [Task] t ON ut.TaskId=t.TaskId JOIN [TaskState] ts ON ut.StateId=ts.StateId
