---
name: code-review
description: Review code changes for concrete correctness, regression, and security issues.
---

# Code review

Review the proposed changes against their base and inspect nearby code when needed to understand their behavior.

- Report only actionable issues introduced by the changes; do not report style preferences or pre-existing problems.
- Verify each concern against the code and describe a concrete scenario in which it causes incorrect behavior or a security risk.
- Prioritize findings by severity and include the affected file and line.
- Keep findings concise and explain the impact. If no actionable issues are found, say so.
- Do not modify the code while reviewing.
