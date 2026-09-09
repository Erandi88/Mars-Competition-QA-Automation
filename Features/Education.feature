Feature: Education

Background:
    Given I am logged in to Mars
    And I am on the Education tab

@education @positive
Scenario: Add Education with valid details
    Given Education from "validAdd" does not exist
    When I add Education using "validAdd"
    Then the Education from "validAdd" should be displayed

@education @positive
Scenario: Edit an existing Education with valid details
    Given Education from "validEditUpdated" does not exist
    And Education from "validEditExisting" exists
    When I update Education from "validEditExisting" using "validEditUpdated"
    Then the Education from "validEditUpdated" should be displayed
    And the Education from "validEditExisting" should not be displayed

@education @positive
Scenario: Delete an existing Education with valid details
    Given Education from "validDelete" exists
    When I delete Education from "validDelete"
    Then the Education from "validDelete" should not be displayed