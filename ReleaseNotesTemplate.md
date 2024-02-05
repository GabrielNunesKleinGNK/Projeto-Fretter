## Build {{buildDetails.buildNumber}}
* **Branch**: {{buildDetails.sourceBranch}}
* **Versão**: {{buildDetails.buildNumber}}

## Work Items
{{#if this.workItems}}
{{#forEach this.workItems}}
*  [**{{this.id}}**]({{replace this.url "_apis/wit/workItems" "_workitems/edit/"}}):  {{lookup this.fields 'System.Title'}} `{{lookup this.fields 'System.WorkItemType'}}`
{{/forEach}}
{{else}}
    Esta versão não contém itens linkados, entrar em contato para detalhes.
{{/if}}