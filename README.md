# Assignment #4

## C&#35;

Clone this repository and bring the code pieces you need into your BDSA Assignments GitHub repository.

### Kanban Board

[![Simple-kanban-board-](https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Simple-kanban-board-.jpg/512px-Simple-kanban-board-.jpg)](https://commons.wikimedia.org/wiki/File:Simple-kanban-board-.jpg "Jeff.lasovski [CC BY-SA 3.0 (https://creativecommons.org/licenses/by-sa/3.0)], via Wikimedia Commons")

You are required to implement a model using Entity Framework Core for a simple kanban board for tracking work progress.

1. Install the required `Microsoft.EntityFrameworkCore` package.

1. Setup and configure your database host of choice.

1. Implement the following entities (*POCOs*) in the `Entities` folder.

    - Task
        - Id : int
        - Title : string(100), required
        - AssignedTo : optional reference to *User* entity
        - Description : string(max), optional
        - State : enum (New, Active, Resolved, Closed, Removed), required
        - Tags : many-to-many reference to *Tag* entity
    - User
        - Id : int
        - Name : string(100), required
        - Email : string(100), required, unique
        - Tasks : list of *Task* entities belonging to *User*
    - Tag
        - Id : int
        - Name : string(50), required, unique
        - Tasks : many-to-many reference to *Task* entity

1. Implement the `KanbanContext` required for the model above in the `Entities` folder.

1. Implement and test the `TaskRepository` class.

#### Notes

Override the `OnModelCreating` method on the `KanbanContext` and use it to seed your database with data so you can test and implement the `TaskRepository` class. See: <https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding> and <https://mockaroo.com/>.

Ensure that the `State` property of the `Task` entity is stored as a `string`. See: <https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions>.
