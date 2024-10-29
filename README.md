
# Log Finder
The **Log Finder** is designed to search through logs using queries.

---

## Getting Started

Follow the instructions below to run the app.

### Prerequisites

Ensure the following are installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

---

## How to Run

### 1. Clone the Repository

Clone the repository to your local machine.

```bash
git clone https://github.com/arnask/LogFinder.git
```

### 2. Start Docker and Navigate to the Project Directory

Ensure Docker is running, then open a terminal and navigate to the project directory:

```bash
cd path-to-the-project/LogFinder
```

### 3. Run the MongoDB Database with Docker Compose

To run the MongoDB database in a Docker container, use the following command:

```bash
docker-compose up -d
```

### 4. Build and Run the .NET Application

Navigate to the folder containing the .NET console application, then build and run it:

```bash
cd path-to-the-project/LogFinder/src/LogFinder
dotnet build
dotnet run
```
---

## Query Formats

### Regular Queries
- Format: `[column_name = 'search_string']`
- Example: `signatureId='Microsoft-Windows-Security-Auditing:4608'`

### Using Partial Text Search
Use wildcards (`*`) to perform partial text searches.

- Start of Text: `signatureId='4608*'`
- End of Text: `signatureId='*4608'`
- Anywhere in Text: `signatureId='*4608*'`

### Bool Operators
Use bool operators to combine multiple queries. Supported bool operators are: `AND`, `OR`, or `NOT`.

  ```
  signatureId='Microsoft-Windows-Security-Auditing:4608' AND severity='3'
  signatureId='Microsoft-Windows-Security-Auditing:4608' OR severity='3'
  signatureId='Microsoft-Windows-Security-Auditing:4608' NOT severity='3'
  ```

### Alerts Based on Severity
To trigger alerts based on severity level:

- Format: `[column_name = 'search_string' alert]`
- Example: `signatureId='Microsoft-Windows-Security-Auditing:4608' alert`

---

## SQL Query Support

The application also supports SQL-like queries for searching logs.

### Basic SQL Query
```sql
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608'
```

### Using Partial Text Search with SQL
- Start of Text: `SELECT FROM WHERE signatureId = '4608*'`
- End of Text: `SELECT FROM WHERE signatureId = '*4608'`
- Anywhere in Text: `SELECT FROM WHERE signatureId = '*4608*'`

### SQL with Bool Operators
```sql
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' AND severity = '3'
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' OR severity = '3'
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' NOT severity = '3'
```

### SQL Alerts Based on Severity
To create alerts using SQL syntax:

```sql
SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' alert
```

---


## Changing the Files Directory

To change the directory for log files used by the application, you have two options:

### Option 1: Use the command Line
You can change the directory in the running application by writing `cd` command combined with the desired path (only absolute paths are available).

```bash
cd path/to/csv/files
```
### Option 2: Update the appsettings.json file
You can update the directory path directly in the appsettings.json file:

```
"Directory": {
  "Path": "path/to/csv/files"
}
```
---
