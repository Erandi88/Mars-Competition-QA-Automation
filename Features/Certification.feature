Feature: Certification

Background:
    Given I am logged in to Mars
    And I am on the Certification tab

@certification @positive
Scenario: Add Certification with valid details
    Given Certification from "validAdd" does not exist
    When I add Certification using "validAdd"
    Then the Certification from "validAdd" should be displayed

