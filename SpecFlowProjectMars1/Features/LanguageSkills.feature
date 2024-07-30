Feature: Manage Languages
  As a user
  I want to add new languages to my profile
  So that I can showcase the languages I know
 @language
  Scenario Outline: Add a new language
    Given the user logs into ProjectMars
    When the user adds the language "<languages>"  with level "<level>"
  Then the "<languages>"should be added to the list

    Examples: 
      | languages  | level  |
      |            | Fluent |
      | Spanish12    | Basic  |
      | Special@#  | Fluent |

 @language
 Scenario: Update the language name
 Given the user logs into ProjectMars
 When I add a language "<oldLang>" with level "<oldLevel>"
And I update "<oldLang>" to "<newLang>"
Then "<newLang>" is added to list
Examples: 
| oldLang | oldLevel | newLang |
| French  | Basic    | Hindi   |
| German  | Fluent   | @#$%abc |

 @language
  Scenario: Delete a language
    Given the user logs into ProjectMars
    When I add a language "<language1>" with level "<level1>"
    And I delete the language "<language1>"
    Then the language "<language1>" is removed
    Examples: 
    | language1 | level1 |
    | abcdefghi | Basic  |
    | 12345     | Fluent |
@skill
 Scenario Outline: Add a skill
 Given the user logs into ProjectMars
 When the user adds a skill "<skills>" with level "<levels>"
 Then the "<skills>" should be added to the list
 Examples: 
 | skills   | levels   |
 | c#       | Expert   |
 | skill123 | Beginner |
 |          |  Expert  |
 @skill
 Scenario Outline: Update a skill
 Given the user logs into ProjectMars
 When  add a skill "<oldSkills>" with level "<oldLevels>"
 And I update the "<oldSkills>" to "<newSkills>"
 Then "<oldSkills>"  is updated to "<newSkills>"
 Examples: 
 | oldSkills   | oldLevels | newSkills |
 | python1     | Expert    | Testing   |
 | gfgfch21 | Beginner  | JIRA      |
 @skill
 Scenario Outline: Delete a skill
 Given the user logs into ProjectMars
 When I add a skill "<skillName1>" with level "<levelName1>"
 And I delete the "<skillName1>"
 Then the skill "<skillName1>" is removed
 Examples: 
 | skillName1        | levelName1 |
 | abcdefghijklmnopq | Beginner   |
 | java              | Expert     |