CREATE VIEW [dbo].[vUserProgress]
	AS SELECT ISNULL(up.UserId, -999) AS UserId,
	ISNULL(t.TaskId, -999) AS TaskId,
	ISNULL(tr.TaskTrackId, -999) AS TaskTrackId,
	CONCAT(up.Name, ' ', up.LastName) AS UserName,
	t.Name AS TaskName,
	tr.TrackNote AS TrackNote,
	tr.TrackDate AS TrackDate
	FROM [UserProfile] up, [Task] t, [TaskTrack] tr