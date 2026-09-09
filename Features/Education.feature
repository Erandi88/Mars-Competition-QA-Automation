Feature: Education

  Background:
    Given I am logged in to Mars
    And I am on the Education tab

  @education @positive
  Scenario: Add Education with valid details
    Given Education from "validAdd" does not exist
    When I add Education using "validAdd"
    Then the Education from "validAdd" should be displayed
 