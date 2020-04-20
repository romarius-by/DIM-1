﻿using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class TaskTrackService : ITaskTrackService
    {

        private IUnitOfWork database;

        public TaskTrackService(IUnitOfWork uow)
        {
            database = uow;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task track id value is not set", String.Empty);

            database.TaskTracks.DeleteById(id.Value);
            database.Save();
        }

        public async Task<OperationDetails> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The id value is not set!", String.Empty);

            var res = await database.TaskTracks.DeleteByIdAsync(id.Value);

            if (res != null)
                return new OperationDetails(true, "Task track has been succesfully deleted: ", res.TrackNote);

            else
                return new OperationDetails(false, "Something went wrong!", " ");
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public TaskTrackDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task track id value is not set", String.Empty);

            var task = database.TaskTracks.GetById(id.Value);

            if (task == null)
                throw new ValidationException($"The task track with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<TaskTrack, TaskTrackDTO>(task);
        }

        public IEnumerable<TaskTrackDTO> GetAll()
        {
            return Mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                database.TaskTracks.GetAll().ToList());
        }

        public UserTaskDTO GetUserTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task track id value is not set", String.Empty);

            return Mapper.Map<UserTask, UserTaskDTO>(
                database.TaskTracks.GetById(id.Value).UserTask);
        }

        public void Save(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = new TaskTrack
            {
                TrackNote = taskTrackDTO.TrackNote,
                TrackDate = taskTrackDTO.TrackDate,
                UserTask = Mapper.Map<UserTaskDTO, UserTask>(taskTrackDTO.UserTask),
                UserTaskId = taskTrackDTO.UserTaskId
            };

            database.TaskTracks.Create(taskTrack);
            database.Save();
        }

        public void Update(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = database.TaskTracks.GetById(taskTrackDTO.TaskTrackId);

            if (taskTrack != null)
            {
                Mapper.Map(taskTrackDTO, taskTrack);
                database.Save();
            }
        }
    }
}
