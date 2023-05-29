# SuperSold

SuperSold is a marketplace website built on ASP.NET Core MVC. It allows users to register, upload products, and engage in simulated buying and selling experiences. The platform features user authentication, product management, and a responsive interface.

Please note that SuperSold is a learning project and does not execute actual financial transactions.

## Features

- User Registration: Users can create an account to access additional features and interact with the marketplace.
- Product Management: Users can upload, modify, and delete their own products.
- Anonymous Product Browsing: The front page displays all existing products, allowing anonymous users to browse the available listings.
- Cart and Wishlist: Logged-in users can add products to their cart or wishlist for future reference.
- Purchase Page: The purchase page enables users to simulate the payment process for their selected items. The page displays products, quantities, and a total cost summary.
- Admin Panel: Accounts with the "admin" role have access to an admin-only area, where they can delete products and restrict user capabilities.
- Theme Toggle: The site offers both light and dark themes, which can be switched using the dropdown menu in the navbar.

## Usage

To use SuperSold, please follow the steps below:

1. Clone the repository to your local machine.
2. Make sure you have MySQL installed on your system.
3. Import the database structure using the provided .mwb file located at the root of the repository. You can import it into MySQL Workbench by following these steps:
   - Open the EER Diagram by using Supersold_MySQL_Schema.mwb in the root of the repository with MySQL Workbench.
   - Go to the "Database" menu and select "Forward Engineer".
   - Review the database structure and proceed with the import.
4. Set up the user secrets configuration for the AspDotNet project. The configuration should include the following settings:
   - `EmailAuth:Username`: Replace `{username}` with your MailTrap SMTP username credentials.
   - `EmailAuth:Password`: Replace `{password}` with your MailTrap SMTP password credentials.
   - `ConnectionString:MySQL`: Replace `{connection string}` with your MySQL database connection string.
5. Build and run the AspDotNet project using your preferred development environment or command-line interface.
