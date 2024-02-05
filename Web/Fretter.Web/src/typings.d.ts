/* SystemJS module definition */
declare var module: NodeModule;
interface NodeModule {
	id: string;
}

declare module "*.json" {
	const value: any;
	export default value;
}

declare var mMenu: any;
declare var mOffcanvas: any;
declare var mScrollTop: any;
declare var mHeader: any;
declare var mToggle: any;
declare var mQuicksearch: any;
declare var mUtil: any;
declare var mPortlet: any;
