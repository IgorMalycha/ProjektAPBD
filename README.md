# Revenue Recognition

This project implements a Revenue Recognition System using C# and ASP.NET in a REST API architecture, following the MVC pattern. The system addresses problem of recognizing revenue for product (software) purchases and services (software subscriptions), ensuring that revenue is recorded correctly according to applicable regulations and business standards.

Key functionalities include:

- Customer Management: Supports adding, updating, and soft-deleting customer records. Customers can be individuals or companies, each with specific required fields (e.g., PESEL for individuals, KRS for companies). Only administrators can perform deletion and updates.

- Software License Management: Handles contracts for software purchases, including subscription-based and one-time purchases, with support for discounts, version tracking, and license extensions.

- Contract Handling: Allows creating contracts with customizable payment periods and terms, ensuring the correct application of discounts for returning customers or promotions. The contract’s value is only recognized as revenue after full payment is received.

- Revenue Calculation: Provides endpoints for calculating both actual and expected revenue. This includes support for currency conversion using public exchange rate APIs to accommodate different currencies.

The project adheres to best practices such as clean code, SOLID principles, and incorporates unit tests for business logic validation. It includes secure user authentication, ensuring only authorized employees can manage data, with role-based access for administrators and standard users. Communication with the database is accomplished via an ORM, specifically Entity Framework.





