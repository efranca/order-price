# Calculate the price of an order

Given a CSV file with the following lines:
```
<ProductID>,<Stock>,<Price>
```

Write a command line program that calculates the total price of an order given the following rules:
* The total amount to be paid is the sum of the price of each product in the order multiplied by the quantity of each item in the order
* Prices on the CSV file do not include VAT
* The total amount to be paid must include VAT at the fixed rate of 23%
* If a product is out of stock, the program must end with error code 1 and display a message

## Command line interface
The program must run from the command line with the following arguments
```
CalculateOrder Path_to_catalog Product1 Quantity_P1 <Product2 Quantity_P2> ...
```

## Example
Given the input `Catalog.txt` file
```
P4,10,250.00
P10,5,175.00
P12,5,1000.00
```

```
$ CalculateOrder Catalog.txt P4 6 P10 5 P12 1
Total: 4151,25
```

## Deliverable
We expect you to deliver a zip file containing the code that implements the solution for this problem.
Plese provide clear instructions on how to build the application.

## Programming languages
We accept solutions implemented in one of the following programming languages:
* C#
* Java
* Kotlin
* Javascript
* Python