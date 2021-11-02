db = db.getSiblingDB("ToDoStoreDb");

db.createCollection("ToDos");

db.ToDos.insertMany([
  {
    description: "A very very long task",
    isDone: false,
  },
  {
    description: "Short stuff",
    isDone: true,
  },
  {
    description: "Try to initialize your mongodb with this script",
    isDone: false,
  },
]);
