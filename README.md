# Maiden-DataVisualizer

var config = {
    entityLayout: {
        LIST: 'List',
        FORM: 'Form'
    },
    fieldType: {
        STRING: { type: 'string' },
        NUMBER: { type: 'number'}
    },
    defConfig: {
        entity: { type: this.entityLayout.LIST },
        detail: { type: this.fieldType.STRING}
    },
	entities: [ 
		{ id: 1, name: 'FriendManagement', isMenu: true, isAppBar: true },
		{ id: 2, name: 'OfficeMangement'}
	],
    details: [
        { id: 1, name: 'Name', entity: 1 },
        { id: 2, name: 'PhoneNumber', entity: 1 },
        { id: 3, name: 'Name', entity: 2 },
        { id: 4, name: 'Experience', entity: 2, type: this.fieldType.NUMBER }
    ]

}

var menuItems = config.entities.filter(item => item.isMenu);
var appBarItems = config.entities.filter(item => item.isAppBar);
var entities = config.entities.map(entity => { ...defConfig.entity, ...entity});
var details = config.details.map(detail => { ...defConfig.detail, ...detail } );

var friendManagementDetails = details.filter(item => item.id == 1 );
var officeManagementDetails = details.filter(item => item.id == 2 );


Base
	ID
	Created
	Modified
	
FriendManagement : Base
	Name
	PhoneNumber
	
OfficeMangement : Base
	Name
	Exp
	