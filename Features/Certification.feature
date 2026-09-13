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

