SELECT setval('"Employee_Id_seq"', (SELECT max("Id") FROM public."Employee"));

SELECT setval('"EmployeeSalary_Id_seq"', (SELECT max("Id") FROM public."EmployeeSalary"));