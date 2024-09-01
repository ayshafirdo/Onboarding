Feature: Manage Languages
  As a successfully logged in user
  I want to manage languages in my profile
  So that I can showcase the languages I know
 @language
  Scenario Outline: Add a new language
  Given the user is on the Languages page
    When the user adds the language "<languages>"  with level "<level>"
  Then the "<languages>"should be added to the list

    Examples: 
      | languages  | level  |
      |            | Fluent |
      | Spanish12  | Basic  |
      | Special@#  | Fluent |

 @language
 Scenario: Update the language name
 Given the user is on the Languages page
 When I add a language "<oldLang>" with level "<oldLevel>"
And I update "<oldLang>" to "<newLang>"
Then "<newLang>" is added to list
Examples: 
| oldLang | oldLevel | newLang |
| French  | Basic    | Hindi   |
| German  | Fluent   | @#$%abc |

 @language
  Scenario: Delete a language
  Given the user is on the Languages page
    When I add a language "<language1>" with level "<level1>"
    And I delete the language "<language1>"
    Then the language "<language1>" is removed
    Examples: 
    | language1 | level1 |
    | abcdefghi | Basic  |
    | 12345     | Fluent |

@language
Scenario:Verify user can not add a language without choosing a language level
Given the user is on the Languages page
When I attempt to add a language "Arabic" without selecting a level
Then I should see an error message "Please enter language and level"

@language
Scenario:Verify user cannot add a language and language level combination that already exists
Given the user is on the Languages page
Given the language "abcd" with level "Fluent" is already present
When I attempt to add the language "abcd" with level "Fluent"
Then I should see an error message saying "This language is already exists in your language list"

@language
Scenario: Verify user cannot add the same language with a different language level
Given the user is on the Languages page
    Given the language "English" with level "Fluent" is already present
    When I attempt to add the language "English" with level "Conversational"
    Then I should see an error message "Duplicated data"

@language
Scenario: Verify that the system can gracefully handle large payload when adding a language
Given the user is on the Languages page
    When I attempt to add a language with a large payload
    Then the system should gracefully handle the large payload without errors 

@language
Scenario:Verify user cannot update a duplicate language name
Given the user is on the Languages page
Given I have a language named "Tamil" with level "Basic" in the system
    When I attempt to update the language name "Tamil" to "Spanish"
    And I attempt to update the language name "Spanish"again to "Spanish"
    Then the system should display an error message "This language is already added to your language list."
    



