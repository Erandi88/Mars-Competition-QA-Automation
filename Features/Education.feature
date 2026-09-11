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

@education @positive
Scenario: Cancel editing an existing Education
    Given Education from "cancelEditExisting" exists
    And Education from "cancelEditUpdated" does not exist
    When I edit Education from "cancelEditExisting" using "cancelEditUpdated" and cancel the changes
    Then the Education from "cancelEditExisting" should be displayed
    And the Education from "cancelEditUpdated" should not be displayed


@education @negative @validinput
Scenario: Update Education to match another existing Education
    Given Education from "duplicateEditSource" exists
    And Education from "duplicateEditTarget" exists
    When I update Education from "duplicateEditSource" using "duplicateEditTarget"
    Then the duplicate Education message should be displayed
    And only one Education from "duplicateEditTarget" should be displayed
    When I cancel the education edit
    Then the Education from "duplicateEditSource" should be displayed


@education @negative @invalidinput
Scenario Outline: Add Education with missing required field
    Given Education from "<dataKey>" does not exist
    When I attempt to add Education using "<dataKey>"
    Then the Education validation message should be displayed
    And no Education record should be created

Examples:
    | dataKey               |
    | missingUniversity     |
    | missingCountry        |
    | missingTitle          |
    | missingDegree         |
    | missingGraduationYear |


@education @negative @invalidinput
Scenario Outline: Add Education with spaces-only text field
    Given Education from "<dataKey>" does not exist
    When I attempt to add Education using "<dataKey>"
    Then the invalid Education message should be displayed
    And no Education record should be created

Examples:
    | dataKey              |
    | spacesOnlyUniversity |
    | spacesOnlyDegree     |