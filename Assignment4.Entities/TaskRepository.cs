using System.Collections.Generic;
using System.IO;
using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Assignment4;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Immutable;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {

        KanbanContext context;


        public TaskRepository()
        {
            var connectionString = "Server=localhost;Database=Kanban-Board;User Id=sa;Password=34a6e100-d809-488a-9ce4-cfd2aeac387b";

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlServer(connectionString);
            context = new KanbanContext(optionsBuilder.Options);
        }

        public IReadOnlyCollection<TaskDTO> All()
        {
            // var taskList = context.Tasks.ToList();

            // System.Console.WriteLine(taskList);

            // var taskDTOList = new List<TaskDTO>();
            // foreach (var t in taskList) {
            //     var taskDTO = new TaskDTO{Id = t.ID, 
            //     AssignedToId = t.AssignedTo.ID, 
            //     Description = t.Description, 
            //     State = t.State, 
            //     Tags = (IReadOnlyCollection<string>)t.Tags, 
            //     Title = t.Title};

            //     taskDTOList.Add(taskDTO);
            // }

            var tasks = context.Tasks;
            var list = tasks.Select(t => new TaskDTO {
                Id = t.ID,
                AssignedToId = t.AssignedTo.ID,
                Description = t.Description,
                State = t.State,
                Tags = t.Tags.Select(tag => tag.ToString()).ToList().AsReadOnly(),
                Title = t.Title
            });

            return list.ToList<TaskDTO>().AsReadOnly();
        }

        public int Create(TaskDTO task)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TaskDTO task)
        {
            throw new System.NotImplementedException();
        }
    }
}
