# Sauce Demo Test Automation Project

## Overview
This project provides automated testing for the Sauce Demo login functionality using C# with Selenium WebDriver and supporting parallel execution.

## Test Cases Covered

### UC-1: Test Login form with empty credentials
- Enter credentials in both Username and Password fields
- Clear both inputs
- Click Login button
- Verify error message: "Username is required"

### UC-2: Test Login form with Username only
- Enter any username
- Enter password
- Clear the Password input
- Click Login button
- Verify error message: "Password is required"

### UC-3: Test Login form with valid credentials
- Enter valid username (from accepted usernames list)
- Enter password as "secret_sauce"
- Click Login button
- Verify dashboard title "Swag Labs"

## Technology Stack

- **Framework**: .NET 8
- **Test Runner**: xUnit
- **Web Automation**: Selenium WebDriver
- **Browsers**: Chrome, Firefox, Edge
- **Locators**: XPath
- **Assertions**: FluentAssertions