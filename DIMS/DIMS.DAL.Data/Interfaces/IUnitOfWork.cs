﻿using DIMS.EF.DAL.Data.Interfaces;
using System;

namespace DIMS.EF.DAL.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sample> Samples { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Direction> Directions { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TaskState> TaskStates { get; }
        IRepository<TaskTrack> TaskTracks { get; }
        IRepository<UserTask> UserTasks { get; }
        IViewRepository<vTask> vTasks { get; }
        IvUserProfileRepository vUserProfiles { get; }
        IViewRepository<vUserProgress> vUserProgresses { get; }
        IViewRepository<vUserTask> vUserTasks { get; }
        IViewRepository<vUserTrack> vUserTracks { get; }
        void Save();
    }
}
