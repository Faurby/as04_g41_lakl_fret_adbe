# Assignment #4

## C&#35;

Clone this repository and bring the code pieces you need into your BDSA Assignments GitHub repository.

### Kanban Board

[![Simple-kanban-board-](https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Simple-kanban-board-.jpg/512px-Simple-kanban-board-.jpg)](https://commons.wikimedia.org/wiki/File:Simple-kanban-board-.jpg "Jeff.lasovski [CC BY-SA 3.0 (https://creativecommons.org/licenses/by-sa/3.0)], via Wikimedia Commons")

You are required to implement a model using Entity Framework Core for a simple kanban board for tracking work progress.

1. Install the required `Microsoft.EntityFrameworkCore` package.

1. Setup and configure your database host of choice.

1. Implement the following entities (*POCOs*) in the `Entities` project.

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

1. Ensure that the `State` property of the `Task` entity is stored as a `string`. See: <https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions>.

1. Implement the `KanbanContext` required for the model above in the `Entities` project.

1. Implement and test the `ITagRepository` *and/or* the `IUserRepository` using the classes in the `Entities` project

1. Implement and test the `ITaskRepository` interface in the `Core` project using the `TaskRepository` class in the `Entities` project.

### Business Rules

#### 1. General

1. Trying to update or delete a non-existing entity should return `NotFound`.
1. *Create*, *Read*, and *Update* should return a proper `Response`.
1. Your are not allowed to write `throw new ...` - use the `Response` instead.
1. Your code must use an in-memory database for testing.
1. If a task, tag, or user is not found, return `null`.

#### 2. Task Repository

1. Only tasks with the state `New` can be deleted from the database.
1. Deleting a task which is `Active` should set its state to `Removed`.
1. Deleting a task which is `Resolved`, `Closed`, or `Removed` should return `Conflict`.
1. Creating a task will set its state to `New` and `Created`/`StateUpdated` to current time in UTC.
1. Create/update task must allow for editing tags.
1. Updating the `State` of a task will change the `StateUpdated` to current time in UTC.
1. Assigning a user which does not exist should return `BadRequest`.
1. TaskRepository may *not* depend on *TagRepository* or *UserRepository*.

#### 3. Tag Repository

1. Tags which are assigned to a task may only be deleted using the `force`.
1. Trying to delete a tag in use without the `force` should return `Conflict`.
1. Trying to create a tag which exists already should return `Conflict`.

#### 4. User Repository

1. Users who are assigned to a task may only be deleted using the `force`.
1. Trying to delete a user in use without the `force` should return `Conflict`.
1. Trying to create a user which exists already (same email) should return `Conflict`.

### Notes

Comparing actual time in a unit test can be tricky - `DateTime.UtcNow` is too precise - so setting a value in a test to compare with value in code will not work.

You will want to allow timing to be slightly off - maybe by 5 seconds as the following snippet demonstrates:

```csharp
var expected = DateTime.UtcNow;
var actual = DateTime.UtcNow.AddSeconds(2);

Assert.Equal(expected, actual, precision: TimeSpan.FromSeconds(5)); // true
```

## Software Engineering

### Exercise 1

Study the meaning of encapsulation, inheritance, and polymorphism in OO languages.  Provide a description of these concepts including UML diagram showcasing them.

### Exercise 2

As a maintainance activity (i.e., after having written the code): draw a class diagram representing your implementation of the entities for the C# part.  The purpose of the diagram should be to `document` the main relationships between the entities and their multiplicity.

### Exercise 3

As a maintainance activity (i.e., after having written the code): draw a state machine diagram representing your implementation of the task entity.  The purpose of the diagram should be to `document` the diffent states the entity can go through and the transitions that trigget the changes.

### Exercise 4

For each of the SOLID principles, provide an example through either a UML diagram or a code listing that showcases the violation of the specific principle.
_Note:_ the examples do not need to be sophisticated.

### Exercise 5

For each of the examples provided for SE-4, provide a refactored solution as either a UML diagram or a code listing that showcases a possible solution respecting the principle violeted.
_Note:_ the examples do not need to be sophisticated.

### Note

Exercises 4 and 5 could be used in class to further discuss additional examples of the SOLID principles.  If you wish to present and discuss your examples discussed in class, I would be happy to include an hour in one of the future lectures.  Therefore, if the above sounds interesting, please do not hesitate to contact me.  The idea would be to have a brief slot in which you would present your example of one or more of the SOLID principles.

## Submitting the assignment

To submit the assignment you need to create a .pdf document using LaTeX containing the answers to the SE questions and a link to a public repository containing your fork of the completed code with the solutions to the C# part.

Members of the triplets should submit the same PDF file to pass the assignments.  Make sure all group names and ID are clearly marked on the front page.
