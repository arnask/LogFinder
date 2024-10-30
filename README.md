# Log Finder
The **Log Finder** is designed to search through logs using queries.

---

## Getting started

Follow the instructions below to run the app.

### Prerequisites

Ensure the following are installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

---

## How to run

### 1. Clone the repository

Clone the repository to your machine.

```bash
git clone https://github.com/arnask/LogFinder.git
```

### 2. Start Docker and navigate to the project directory

Ensure Docker is running. Then open a terminal and navigate to the project directory, where docker-compose.yml is located:

```bash
cd path-to-the-project/LogFinder
```

### 3. Run the MongoDB database in Docker container

To run the MongoDB database in a Docker container, use the following command:

```bash
docker-compose up -d
```

### 4. Build and run the .NET application

Navigate to the folder containing the .NET console application, then build and run it:

```bash
cd path-to-the-project/LogFinder/src/LogFinder
dotnet build
dotnet run
```
---

## Query Formats

### Regular queries
- Format: `[column_name = 'search_string']`
- Example: `signatureId='Microsoft-Windows-Security-Auditing:4608'`

### Using partial text search
Use wildcards (`*`) to perform partial text searches.

- Start of text: `signatureId='4608*'`
- End of text: `signatureId='*4608'`
- Anywhere in text: `signatureId='*4608*'`

### Bool operators
Use bool operators to combine multiple queries. Supported bool operators are: `AND`, `OR`, or `NOT`.

  ```
  signatureId='Microsoft-Windows-Security-Auditing:4608' AND severity='3'
  signatureId='Microsoft-Windows-Security-Auditing:4608' OR severity='3'
  signatureId='Microsoft-Windows-Security-Auditing:4608' NOT severity='3'
  ```

### Alerts
To trigger alerts based on severity level, add "alert" keyword to the end of the query:

- Format: `[column_name = 'search_string' alert]`
- Example: `signatureId='Microsoft-Windows-Security-Auditing:4608' alert`

---

## SQL-like queries

### Basic query

```sql
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608'
```

### Using partial text search
- Start of text: `SELECT FROM WHERE signatureId = '4608*'`
- End of text: `SELECT FROM WHERE signatureId = '*4608'`
- Anywhere in text: `SELECT FROM WHERE signatureId = '*4608*'`

### Bool operators

```sql
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' AND severity = '3'
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' OR severity = '3'
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' NOT severity = '3'
```

### Alerts
To trigger alerts based on severity level, add "alert" keyword to the end of the query:

```sql
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' alert
```

---


## Changing the Files Directory

To change the directory for log files used by the application, you have two options:

### Option 1: Use the command line
You can change the directory in the running application by writing `cd` command combined with the desired path (only absolute paths are available).

```bash
cd path/to/csv/files
```
### Option 2: Update the appsettings.json file
You can update the directory path directly in the appsettings.json file (the application needs to be restarted after this):

```
"Directory": {
  "Path": "path/to/csv/files"
}
```
---
