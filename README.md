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

1. Implement and test the `ITaskRepository` interface in the `Core` project using the `TaskRepository` class in the `Entities` project.

#### Notes

To test your repository you may want to generate some test data.

Consider *seeding* or *custom initialization logic*, cf. <https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding> and <https://mockaroo.com/>.

You may want to ensure that your tests are run in a specific order: <https://docs.microsoft.com/en-us/dotnet/core/testing/order-unit-tests?pivots=xunit>.










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



