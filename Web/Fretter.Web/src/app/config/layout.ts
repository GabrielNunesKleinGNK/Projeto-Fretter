import { ConfigModel } from '../core/interfaces/config';

export class LayoutConfig implements ConfigModel {
	public config: any = {
		"demo": "default",
		"self": {
		  "layout": "fluid",
		  "background": "./assets/app/media/img/bg/bg-2.jpg"
		},
		"loader": {
		  "enabled": true
		},
		"header": {
		  "self": {
			"fixed": {
			  "desktop": true,
			  "mobile": true,
			  "minimize": {
				"desktop": {
				  "enabled": true,
				  "offset": 200
				},
				"mobile": {
				  "enabled": false,
				  "offset": 200
				}
			  }
			},
			"logo": {
			  "dark": "./assets/app/media/img/logos/logo_verde.png",
			  "light": "./assets/app/media/img/logos/logo_verde.png"
			}
		  },
		  "search": {
			"type": "search-dropdown",
			"dropdown": {
			  "skin": "dark"
			}
		  }
		},
		"aside": {
		  "left": {
			"display": true,
			"fixed": false,
			"skin": "light",
			"push_footer": false,
			"minimize": {
			  "toggle": false,
			  "default": false
			}
		  },
		  "right": {
			"display": false
		  }
		},
		"menu": {
		  "header": {
			"display": false,
			"desktop": {
			  "skin": "dark",
			  "arrow": true,
			  "toggle": "click",
			  "submenu": {
				"skin": "dark",
				"arrow": true
			  }
			},
			"mobile": {
			  "skin": "dark"
			}
		  },
		  "aside": {
			"display": true,
			"desktop_and_mobile": {
			  "submenu": {
				"skin": "dark",
				"accordion": true,
				"dropdown": {
				  "arrow": true,
				  "hover_timeout": 500
				}
			  },
			  "minimize": {
				"submenu_type": "default"
			  }
			}
		  }
		},
		"content": {
		  "skin": "light2"
		},
		"footer": {
		  "fixed": false
		},
		"quicksidebar": {
		  "display": false
		},
		"portlet": {
		  "sticky": {
			"offset": 50
		  }
		}
	  };

	constructor(config?: any) {
		if (config) {
			this.config = config;
		}
	}
}
