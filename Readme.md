# upword.Api

## Description

This is the backend API for the New Word of the Day application. It provides endpoints to manage and retrieve words, helping users improve their vocabulary by learning a new word each day.

## Prerequisites

Before you can run the application, you'll need to install the following tools:

1. **[.NET SDK](https://dotnet.microsoft.com/download)**: The SDK needed to build and run .NET applications.
2. **[Visual Studio Code (VSCode)](https://code.visualstudio.com/)**: A popular code editor.
3. **[Git](https://git-scm.com/)**: A version control system to clone the project repository.

## Installation Steps

Follow these steps to set up and run the application on your local machine:

### 1. Install .NET SDK

1. Download the .NET SDK from the [official website](https://dotnet.microsoft.com/download).
2. Follow the installation instructions for your operating system.

### 2. Install Visual Studio Code (VSCode)

1. Download VSCode from the [official website](https://code.visualstudio.com/).
2. Follow the installation instructions for your operating system.

### 3. Install Git

1. Download Git from the [official website](https://git-scm.com/).
2. Follow the installation instructions for your operating system.

### 4. Clone the Project Repository

1. Open a terminal or command prompt.
2. Clone the project repository:
   ```sh
   git clone https://github.com/sopheak012/upword.Api.git
   ```
3. Navigate to the project directory:
   ```sh
   cd upword.Api
   ```

### 5. Install Project Dependencies

1. Restore the required NuGet packages:
   ```sh
   dotnet restore
   ```

### 6. Run the Application

1. Start the application:
   ```sh
   dotnet run
   ```
2. The API will be running on `http://localhost:5125`.

## Configuration

The application uses SQLite as the database. Ensure the connection string in `appsettings.json` is configured correctly. The frontend application should be configured to make requests to the API running at `http://localhost:5125`.

## REST Client Extension for VSCode

To test the API endpoints, you can use the REST Client extension in Visual Studio Code. Create a file named `word.http` in the project directory with your API requests. Here is an example of what the file might look like:

```
# Get the word of the day
GET http://localhost:5125/words/wordoftheday

### Save this in a file named `word.http` to use with the REST Client extension in VSCode.
```

## Usage

- Ensure both the frontend and backend servers are running.
- Use the REST Client extension in VSCode to test API endpoints by sending HTTP requests defined in the `word.http` file.

## Troubleshooting

If you encounter any issues:

- Ensure all prerequisites are correctly installed.
- Verify that both frontend and backend servers are running.
- Check for any error messages in the terminal and address them as needed.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
