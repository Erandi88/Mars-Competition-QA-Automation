Feature: Mars Login
  As a user, I want to log in to Mars so that I can access my profile.

  @smoke
  Scenario: Perform a successful Mars login
    Given I am on the Mars home page
    When I log in with valid Mars credentials
    Then I should be logged in to Mars