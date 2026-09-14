Feature: Certification

Background:
    Given I am logged in to Mars
    And I am on the Certification tab

@certification @positive
Scenario: Add Certification with valid details
    Given Certification from "validAdd" does not exist
    When I add Certification using "validAdd"
    Then the Certification from "validAdd" should be displayed

@certification @positive
Scenario: Edit an existing Certification with valid details
    Given Certification from "validEditUpdated" does not exist
    And Certification from "validEditExisting" exists
    When I update Certification from "validEditExisting" using "validEditUpdated"
    Then the Certification from "validEditUpdated" should be displayed
    And the Certification from "validEditExisting" should not be displayed

@certification @positive
Scenario: Cancel editing an existing Certification
    Given Certification from "cancelEditUpdated" does not exist
    And Certification from "cancelEditExisting" exists
    When I edit Certification from "cancelEditExisting" using "cancelEditUpdated" and cancel the changes
    Then the Certification from "cancelEditExisting" should be displayed
    And the Certification from "cancelEditUpdated" should not be displayed

@certification @positive
Scenario: Delete an existing Certification
    Given Certification from "validDelete" exists
    When I delete Certification from "validDelete"
    Then the Certification from "validDelete" should not be displayed

@certification @negative @validinput
Scenario: Update Certification to match another existing Certification
    Given Certification from "duplicateEditSource" exists
    And Certification from "duplicateEditTarget" exists
    When I update Certification from "duplicateEditSource" using "duplicateEditTarget"
    Then the duplicate Certification message should be displayed
    And only one Certification from "duplicateEditTarget" should be displayed
    When I cancel the certification edit
    Then the Certification from "duplicateEditSource" should be displayed

@certification @negative @validinput
Scenario: Add an exact duplicate Certification
    Given Certification from "duplicateAdd" exists
    When I add Certification using "duplicateAdd"
    Then the duplicate Certification message should be displayed
    And only one Certification from "duplicateAdd" should be displayed

@certification @negative @invalidinput
Scenario Outline: Add Certification with missing required field
    When I attempt to add Certification using "<dataKey>"
    Then the Certification required-fields message should be displayed
    And no Certification record should be created

Examples:
    | dataKey                    |
    | missingAllFields           |
    | missingYear                |
    | missingCertifiedFrom       |
    | missingCertificate         |


@certification @negative @invalidinput
Scenario Outline: Update Certification with missing required field
    Given Certification from "updateMissingFieldSource" exists
    When I attempt to update Certification from "updateMissingFieldSource" using "<dataKey>"
    Then the Certification required-fields message should be displayed
    When I cancel the certification edit
    Then the Certification from "updateMissingFieldSource" should be displayed

Examples:
    | dataKey                    |
    | updateMissingAllFields     |
    | updateMissingCertificate   |
    | updateMissingCertifiedFrom |
    | updateMissingYear          |



