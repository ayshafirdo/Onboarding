Feature: Mangage Skills
  As a successfully logged in user
  I want to add new skills to my profile
  So that I can showcase the skills I know
  @skill
 Scenario Outline: Add a skill
 
 When the user adds a skill "<skills>" with level "<levels>"
 Then the "<skills>" should be added to the list
 Examples: 
 | skills   | levels   |
 | c#       | Expert   |
 | skill123 | Beginner |
 |          |  Expert  |
 @skill
 Scenario Outline: Update a skill

 When  add a skill "<oldSkills>" with level "<oldLevels>"
 And I update the "<oldSkills>" to "<newSkills>"
 Then "<oldSkills>"  is updated to "<newSkills>"
 Examples: 
 | oldSkills   | oldLevels | newSkills |
 | python1     | Expert    | Testing   |
 | gfgfch21 | Beginner  | JIRA      |
 @skill
 Scenario Outline: Delete a skill

 When I add a skill "<skillName1>" with level "<levelName1>"
 And I delete the "<skillName1>"
 Then the skill "<skillName1>" is removed
 Examples: 
 | skillName1        | levelName1 |
 | abcdefghijklmnopq | Beginner   |
 | java              | Expert     |

 @skill
 Scenario:Verify user can not add a skill without choosing a skill level
 When I attempt to add a skill "Python" without selecting a skill level
Then I should see an error message "Please enter skill and experience level"

@skill
Scenario:Verify user cannot add a skill and skill level combination that already exists
Given the skill "Testing" with level "Beginner" is already present
When I attempt to add the skill "Testing" with level "Beginner"
Then I should see an error message "This skill is already exist in your skill list"

@skill
Scenario: Verify user cannot add the same skill with a different skill level
    Given the skill "Java" with level "Intermediate" is already present
    When I attempt to add the skill "Java" with level "Expert"
    Then I should see an error message "Duplicated data"

    @skill
Scenario: Verify that the system can gracefully handle large payload when adding a skill
    When I attempt to add a skill with a large payload
    Then the system should gracefully handle the large skill payload without errors

    @skill
Scenario:Verify user cannot update a duplicate skill name
Given I have a skill named "Logo" with level "Beginner" in the system
    When I attempt to update the skill name "Logo" to "Web"
    And I attempt to update the skill name "Web"again to "Web"
    Then the system should display an error message "This skill is already added to your skill list."
    
