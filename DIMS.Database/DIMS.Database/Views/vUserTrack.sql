CREATE VIEW [dbo].[vUserTrack]
	AS SELECT ISNULL(ut.UserId, -999) AS UserId,
	ISNULL(ut.TaskId, -999) AS TaskId,
	ISNULL(tr.TaskTrackId, -999) AS TaskTrackId,
	t.Name AS TaskName,
	tr.TrackNote AS TrackNote,
	tr.TrackDate AS TrackDate
	FROM [UserTask] ut JOIN [TaskTrack] tr ON ut.UserTaskId=tr.UserTaskId JOIN [Task] t ON ut.TaskId=t.TaskId