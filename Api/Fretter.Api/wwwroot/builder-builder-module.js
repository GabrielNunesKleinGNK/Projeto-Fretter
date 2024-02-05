(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["builder-builder-module"],{

/***/ "./node_modules/ngx-highlightjs/fesm5/ngx-highlightjs.js":
/*!***************************************************************!*\
  !*** ./node_modules/ngx-highlightjs/fesm5/ngx-highlightjs.js ***!
  \***************************************************************/
/*! exports provided: HighlightModule, HighlightJS, HighlightDirective, ɵa */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HighlightModule", function() { return HighlightModule; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HighlightJS", function() { return HighlightJS; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HighlightDirective", function() { return HighlightDirective; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ɵa", function() { return OPTIONS; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");






/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingReturn,uselessCode} checked by tsc
 */
/** @type {?} */
var OPTIONS = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["InjectionToken"]('OPTIONS');

/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingReturn,uselessCode} checked by tsc
 */
var HighlightJS = /** @class */ (function () {
    function HighlightJS(options, _document) {
        this._document = _document;
        this.options = {
            theme: 'github',
            path: 'assets/lib/hljs',
            auto: true
        };
        this._isReady$ = new rxjs__WEBPACK_IMPORTED_MODULE_3__["BehaviorSubject"](false);
        this.options = Object(tslib__WEBPACK_IMPORTED_MODULE_1__["__assign"])({}, this.options, options);
        this._hljsLoader().subscribe();
    }
    Object.defineProperty(HighlightJS.prototype, "isReady", {
        // Stream that emits when highlightjs is loaded
        get: 
        // Stream that emits when highlightjs is loaded
        /**
         * @return {?}
         */
        function () {
            return this._isReady$.pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["filter"])(function (isReady) { return isReady; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["take"])(1));
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @param {?} name
     * @param {?} value
     * @param {?} ignore_illegals
     * @param {?=} continuation
     * @return {?}
     */
    HighlightJS.prototype.highlight = /**
     * @param {?} name
     * @param {?} value
     * @param {?} ignore_illegals
     * @param {?=} continuation
     * @return {?}
     */
    function (name, value, ignore_illegals, continuation) {
        if (this.hljs) {
            return this.hljs.highlight(name, value, ignore_illegals, continuation);
        }
    };
    /**
     * @param {?} value
     * @param {?} languageSubset
     * @return {?}
     */
    HighlightJS.prototype.highlightAuto = /**
     * @param {?} value
     * @param {?} languageSubset
     * @return {?}
     */
    function (value, languageSubset) {
        if (this.hljs) {
            return this.hljs.highlightAuto(value, languageSubset);
        }
    };
    /**
     * @param {?} value
     * @return {?}
     */
    HighlightJS.prototype.fixMarkup = /**
     * @param {?} value
     * @return {?}
     */
    function (value) {
        if (this.hljs) {
            return this.hljs.fixMarkup(value);
        }
    };
    /**
     * @param {?} block
     * @return {?}
     */
    HighlightJS.prototype.highlightBlock = /**
     * @param {?} block
     * @return {?}
     */
    function (block) {
        if (this.hljs) {
            this.hljs.highlightBlock(block);
        }
    };
    /**
     * @param {?} config
     * @return {?}
     */
    HighlightJS.prototype.configure = /**
     * @param {?} config
     * @return {?}
     */
    function (config) {
        if (this.hljs) {
            this.hljs.configure(config);
        }
    };
    /**
     * @return {?}
     */
    HighlightJS.prototype.initHighlighting = /**
     * @return {?}
     */
    function () {
        if (this.hljs) {
            this.hljs.initHighlighting();
        }
    };
    /**
     * @return {?}
     */
    HighlightJS.prototype.initHighlightingOnLoad = /**
     * @return {?}
     */
    function () {
        if (this.hljs) {
            this.hljs.initHighlightingOnLoad();
        }
    };
    /**
     * @param {?} name
     * @param {?} language
     * @return {?}
     */
    HighlightJS.prototype.registerLanguage = /**
     * @param {?} name
     * @param {?} language
     * @return {?}
     */
    function (name, language) {
        if (this.hljs) {
            this.hljs.registerLanguage(name, language);
        }
    };
    /**
     * @return {?}
     */
    HighlightJS.prototype.listLanguages = /**
     * @return {?}
     */
    function () {
        if (this.hljs) {
            return this.hljs.listLanguages();
        }
    };
    /**
     * @param {?} name
     * @return {?}
     */
    HighlightJS.prototype.getLanguage = /**
     * @param {?} name
     * @return {?}
     */
    function (name) {
        if (this.hljs) {
            return this.hljs.getLanguage(name);
        }
    };
    /**
     * @return {?}
     */
    HighlightJS.prototype._hljsLoader = /**
     * @return {?}
     */
    function () {
        if (this._document.defaultView.hljs) {
            return this._initHLJS();
        }
        else {
            this._themeLoader().subscribe();
            return this._loadScript();
        }
    };
    /**
     * Load hljs script
     */
    /**
     * Load hljs script
     * @return {?}
     */
    HighlightJS.prototype._loadScript = /**
     * Load hljs script
     * @return {?}
     */
    function () {
        var _this = this;
        /** @type {?} */
        var promise = new Promise(function (resolve) {
            /** @type {?} */
            var script = _this._document.createElement('script');
            script.async = true;
            script.type = 'text/javascript';
            script.onload = resolve;
            script.src = _this.options.path + "/highlight.pack.js";
            _this._document.head.appendChild(script);
        });
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["from"])(promise).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["switchMap"])(function () { return _this._initHLJS(); }));
    };
    /**
     * Load hljs theme
     */
    /**
     * Load hljs theme
     * @return {?}
     */
    HighlightJS.prototype._themeLoader = /**
     * Load hljs theme
     * @return {?}
     */
    function () {
        var _this = this;
        /** @type {?} */
        var promise = new Promise(function (resolve) {
            /** @type {?} */
            var style = _this._document.createElement('link');
            style.rel = 'stylesheet';
            style.type = 'text/css';
            style.onload = resolve;
            style.href = _this.options.path + "/styles/" + _this.options.theme + ".css";
            _this._document.head.appendChild(style);
        });
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["from"])(promise);
    };
    /**
     * Initialize hljs on load
     */
    /**
     * Initialize hljs on load
     * @return {?}
     */
    HighlightJS.prototype._initHLJS = /**
     * Initialize hljs on load
     * @return {?}
     */
    function () {
        var _this = this;
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])({}).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["tap"])(function () {
            _this.hljs = _this._document.defaultView.hljs;
            _this.hljs.configure(_this.options.config);
            _this._isReady$.next(true);
        }));
    };
    HighlightJS.decorators = [
        { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"], args: [{
                    providedIn: 'root'
                },] }
    ];
    /** @nocollapse */
    HighlightJS.ctorParameters = function () { return [
        { type: undefined, decorators: [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"] }, { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"], args: [OPTIONS,] }] },
        { type: undefined, decorators: [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"], args: [_angular_common__WEBPACK_IMPORTED_MODULE_2__["DOCUMENT"],] }] }
    ]; };
    /** @nocollapse */ HighlightJS.ngInjectableDef = Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["defineInjectable"])({ factory: function HighlightJS_Factory() { return new HighlightJS(Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["inject"])(OPTIONS, 8), Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["inject"])(_angular_common__WEBPACK_IMPORTED_MODULE_2__["DOCUMENT"])); }, token: HighlightJS, providedIn: "root" });
    return HighlightJS;
}());

/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingReturn,uselessCode} checked by tsc
 */
/**
 * There are 2 ways to higlight a code
 *  1 - using the [code] input (default) <code highlight [code]="yourCode"></code>
 *  2 - using element text content <code> {{yourCode}} </code>
 */
var HighlightDirective = /** @class */ (function () {
    function HighlightDirective(el, renderer, hljs) {
        this.renderer = renderer;
        this.hljs = hljs;
        this.highlighted = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.el = el.nativeElement;
    }
    Object.defineProperty(HighlightDirective.prototype, "setCode", {
        set: /**
         * @param {?} code
         * @return {?}
         */
        function (code) {
            var _this = this;
            this.code = code;
            this.hljs.isReady.subscribe(function () { return _this.highlightElement(_this.el, code); });
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @return {?}
     */
    HighlightDirective.prototype.ngOnInit = /**
     * @return {?}
     */
    function () {
        var _this = this;
        /** Acitvate MutationObserver if `auto` option is true and `[code]` input is not used
         * This will highlight using the text content */
        if (!this.code && this.hljs.options.auto) {
            this.hljs.isReady.subscribe(function () {
                _this.highlightTextContent();
                /** Highlight when text content changes */
                _this.domObs = new MutationObserver(function () { return _this.highlightTextContent(); });
                _this.domObs.observe(_this.el, { childList: true, subtree: true });
            });
        }
    };
    /** Highlight using element text content */
    /**
     * Highlight using element text content
     * @return {?}
     */
    HighlightDirective.prototype.highlightTextContent = /**
     * Highlight using element text content
     * @return {?}
     */
    function () {
        if (!this.highlight) {
            if (this.el.tagName.toLowerCase() === 'code') {
                this.highlightElement(this.el, this.el.innerText.trim());
            }
            else {
                console.warn("[HighlightDirective]: Use 'highlight' on <code> element only");
            }
        }
        else if (this.highlight === 'all') {
            this.highlightChildren(this.el, 'pre code');
        }
        else {
            this.highlightChildren(this.el, this.highlight);
        }
    };
    /** Highlight a code block */
    /**
     * Highlight a code block
     * @param {?} el
     * @param {?} code
     * @return {?}
     */
    HighlightDirective.prototype.highlightElement = /**
     * Highlight a code block
     * @param {?} el
     * @param {?} code
     * @return {?}
     */
    function (el, code) {
        /** @type {?} */
        var res = this.hljs.highlightAuto(code, this.language);
        if (res.value !== el.innerHTML) {
            this.renderer.addClass(el, 'hljs');
            this.renderer.setProperty(el, 'innerHTML', res.value);
            this.highlighted.emit(res);
        }
    };
    /** Highlight multiple code blocks */
    /**
     * Highlight multiple code blocks
     * @param {?} el
     * @param {?} selector
     * @return {?}
     */
    HighlightDirective.prototype.highlightChildren = /**
     * Highlight multiple code blocks
     * @param {?} el
     * @param {?} selector
     * @return {?}
     */
    function (el, selector) {
        var _this = this;
        /** @type {?} */
        var codeElements = el.querySelectorAll(selector);
        /** highlight children with the same selector */
        Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["from"])(codeElements).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["filter"])(function (code) { return code.childNodes.length === 1 && code.childNodes[0].nodeName === '#text'; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (codeElement) { return _this.highlightElement(codeElement, codeElement.innerText.trim()); }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["take"])(1)).subscribe();
    };
    /**
     * @return {?}
     */
    HighlightDirective.prototype.ngOnDestroy = /**
     * @return {?}
     */
    function () {
        /** Disconnect MutationObserver */
        if (!this.code && this.hljs.options.auto) {
            this.domObs.disconnect();
        }
    };
    HighlightDirective.decorators = [
        { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"], args: [{
                    selector: '[highlight]'
                },] }
    ];
    /** @nocollapse */
    HighlightDirective.ctorParameters = function () { return [
        { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] },
        { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Renderer2"] },
        { type: HighlightJS }
    ]; };
    HighlightDirective.propDecorators = {
        highlight: [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"] }],
        language: [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"] }],
        setCode: [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"], args: ['code',] }],
        highlighted: [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"] }]
    };
    return HighlightDirective;
}());

/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingReturn,uselessCode} checked by tsc
 */
var HighlightModule = /** @class */ (function () {
    function HighlightModule() {
    }
    /**
     * @param {?=} options
     * @return {?}
     */
    HighlightModule.forRoot = /**
     * @param {?=} options
     * @return {?}
     */
    function (options) {
        return {
            ngModule: HighlightModule,
            providers: [
                { provide: OPTIONS, useValue: options }
            ]
        };
    };
    HighlightModule.decorators = [
        { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"], args: [{
                    declarations: [HighlightDirective],
                    exports: [HighlightDirective]
                },] }
    ];
    return HighlightModule;
}());

/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingReturn,uselessCode} checked by tsc
 */

/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingReturn,uselessCode} checked by tsc
 */



//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibmd4LWhpZ2hsaWdodGpzLmpzLm1hcCIsInNvdXJjZXMiOlsibmc6Ly9uZ3gtaGlnaGxpZ2h0anMvbGliL2hpZ2hsaWdodC50b2tlbi50cyIsIm5nOi8vbmd4LWhpZ2hsaWdodGpzL2xpYi9oaWdobGlnaHQuc2VydmljZS50cyIsIm5nOi8vbmd4LWhpZ2hsaWdodGpzL2xpYi9oaWdobGlnaHQuZGlyZWN0aXZlLnRzIiwibmc6Ly9uZ3gtaGlnaGxpZ2h0anMvbGliL2hpZ2hsaWdodC5tb2R1bGUudHMiXSwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgSW5qZWN0aW9uVG9rZW4gfSBmcm9tICdAYW5ndWxhci9jb3JlJztcclxuaW1wb3J0IHsgSGlnaGxpZ2h0T3B0aW9ucyB9IGZyb20gJy4vaGlnaGxpZ2h0Lm1vZGVsJztcclxuXHJcbmV4cG9ydCBjb25zdCBPUFRJT05TID0gbmV3IEluamVjdGlvblRva2VuPEhpZ2hsaWdodE9wdGlvbnM+KCdPUFRJT05TJyk7XHJcbiIsImltcG9ydCB7IEluamVjdGFibGUsIEluamVjdCwgT3B0aW9uYWwgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcclxuaW1wb3J0IHsgRE9DVU1FTlQgfSBmcm9tICdAYW5ndWxhci9jb21tb24nO1xyXG5pbXBvcnQgeyBCZWhhdmlvclN1YmplY3QsIE9ic2VydmFibGUsIGZyb20sIG9mIH0gZnJvbSAncnhqcyc7XHJcbmltcG9ydCB7IHRha2UsIGZpbHRlciwgdGFwLCBzd2l0Y2hNYXAgfSBmcm9tICdyeGpzL29wZXJhdG9ycyc7XHJcbmltcG9ydCB7IEhpZ2hsaWdodENvbmZpZywgSGlnaGxpZ2h0T3B0aW9ucywgSGlnaGxpZ2h0UmVzdWx0IH0gZnJvbSAnLi9oaWdobGlnaHQubW9kZWwnO1xyXG5pbXBvcnQgeyBPUFRJT05TIH0gZnJvbSAnLi9oaWdobGlnaHQudG9rZW4nO1xyXG5cclxuQEluamVjdGFibGUoe1xyXG4gIHByb3ZpZGVkSW46ICdyb290J1xyXG59KVxyXG5leHBvcnQgY2xhc3MgSGlnaGxpZ2h0SlMge1xyXG5cclxuICBobGpzOiBhbnk7XHJcblxyXG4gIG9wdGlvbnM6IEhpZ2hsaWdodE9wdGlvbnMgPSB7XHJcbiAgICB0aGVtZTogJ2dpdGh1YicsXHJcbiAgICBwYXRoOiAnYXNzZXRzL2xpYi9obGpzJyxcclxuICAgIGF1dG86IHRydWVcclxuICB9O1xyXG5cclxuICBwcml2YXRlIF9pc1JlYWR5JCA9IG5ldyBCZWhhdmlvclN1YmplY3QoZmFsc2UpO1xyXG5cclxuICAvLyBTdHJlYW0gdGhhdCBlbWl0cyB3aGVuIGhpZ2hsaWdodGpzIGlzIGxvYWRlZFxyXG4gIGdldCBpc1JlYWR5KCk6IE9ic2VydmFibGU8Ym9vbGVhbj4ge1xyXG4gICAgcmV0dXJuIHRoaXMuX2lzUmVhZHkkLnBpcGUoXHJcbiAgICAgIGZpbHRlcihpc1JlYWR5ID0+IGlzUmVhZHkpLFxyXG4gICAgICB0YWtlKDEpXHJcbiAgICApO1xyXG4gIH1cclxuXHJcbiAgY29uc3RydWN0b3IoQE9wdGlvbmFsKCkgQEluamVjdChPUFRJT05TKSBvcHRpb25zOiBIaWdobGlnaHRPcHRpb25zLFxyXG4gICAgICAgICAgICAgIEBJbmplY3QoRE9DVU1FTlQpIHByaXZhdGUgX2RvY3VtZW50OiBhbnkpIHtcclxuICAgIHRoaXMub3B0aW9ucyA9IHsgLi4udGhpcy5vcHRpb25zLCAuLi5vcHRpb25zIH07XHJcbiAgICB0aGlzLl9obGpzTG9hZGVyKCkuc3Vic2NyaWJlKCk7XHJcbiAgfVxyXG5cclxuICBoaWdobGlnaHQobmFtZTogc3RyaW5nLCB2YWx1ZTogc3RyaW5nLCBpZ25vcmVfaWxsZWdhbHM6IGJvb2xlYW4sIGNvbnRpbnVhdGlvbj86IGFueSk6IEhpZ2hsaWdodFJlc3VsdCB7XHJcbiAgICBpZiAodGhpcy5obGpzKSB7XHJcbiAgICAgIHJldHVybiB0aGlzLmhsanMuaGlnaGxpZ2h0KG5hbWUsIHZhbHVlLCBpZ25vcmVfaWxsZWdhbHMsIGNvbnRpbnVhdGlvbik7XHJcbiAgICB9XHJcbiAgfVxyXG5cclxuICBoaWdobGlnaHRBdXRvKHZhbHVlOiBzdHJpbmcsIGxhbmd1YWdlU3Vic2V0OiBzdHJpbmdbXSk6IEhpZ2hsaWdodFJlc3VsdCB7XHJcbiAgICBpZiAodGhpcy5obGpzKSB7XHJcbiAgICAgIHJldHVybiB0aGlzLmhsanMuaGlnaGxpZ2h0QXV0byh2YWx1ZSwgbGFuZ3VhZ2VTdWJzZXQpO1xyXG4gICAgfVxyXG4gIH1cclxuXHJcbiAgZml4TWFya3VwKHZhbHVlOiBzdHJpbmcpOiBzdHJpbmcge1xyXG4gICAgaWYgKHRoaXMuaGxqcykge1xyXG4gICAgICByZXR1cm4gdGhpcy5obGpzLmZpeE1hcmt1cCh2YWx1ZSk7XHJcbiAgICB9XHJcbiAgfVxyXG5cclxuICBoaWdobGlnaHRCbG9jayhibG9jazogSFRNTEVsZW1lbnQpIHtcclxuICAgIGlmICh0aGlzLmhsanMpIHtcclxuICAgICAgdGhpcy5obGpzLmhpZ2hsaWdodEJsb2NrKGJsb2NrKTtcclxuICAgIH1cclxuICB9XHJcblxyXG4gIGNvbmZpZ3VyZShjb25maWc6IEhpZ2hsaWdodENvbmZpZykge1xyXG4gICAgaWYgKHRoaXMuaGxqcykge1xyXG4gICAgICB0aGlzLmhsanMuY29uZmlndXJlKGNvbmZpZyk7XHJcbiAgICB9XHJcbiAgfVxyXG5cclxuICBpbml0SGlnaGxpZ2h0aW5nKCkge1xyXG4gICAgaWYgKHRoaXMuaGxqcykge1xyXG4gICAgICB0aGlzLmhsanMuaW5pdEhpZ2hsaWdodGluZygpO1xyXG4gICAgfVxyXG4gIH1cclxuXHJcbiAgaW5pdEhpZ2hsaWdodGluZ09uTG9hZCgpIHtcclxuICAgIGlmICh0aGlzLmhsanMpIHtcclxuICAgICAgdGhpcy5obGpzLmluaXRIaWdobGlnaHRpbmdPbkxvYWQoKTtcclxuICAgIH1cclxuICB9XHJcblxyXG4gIHJlZ2lzdGVyTGFuZ3VhZ2UobmFtZTogc3RyaW5nLCBsYW5ndWFnZTogRnVuY3Rpb24pIHtcclxuICAgIGlmICh0aGlzLmhsanMpIHtcclxuICAgICAgdGhpcy5obGpzLnJlZ2lzdGVyTGFuZ3VhZ2UobmFtZSwgbGFuZ3VhZ2UpO1xyXG4gICAgfVxyXG4gIH1cclxuXHJcbiAgbGlzdExhbmd1YWdlcygpOiBzdHJpbmdbXSB7XHJcbiAgICBpZiAodGhpcy5obGpzKSB7XHJcbiAgICAgIHJldHVybiB0aGlzLmhsanMubGlzdExhbmd1YWdlcygpO1xyXG4gICAgfVxyXG4gIH1cclxuXHJcbiAgZ2V0TGFuZ3VhZ2UobmFtZTogc3RyaW5nKTogYW55IHtcclxuICAgIGlmICh0aGlzLmhsanMpIHtcclxuICAgICAgcmV0dXJuIHRoaXMuaGxqcy5nZXRMYW5ndWFnZShuYW1lKTtcclxuICAgIH1cclxuICB9XHJcblxyXG4gIHByaXZhdGUgX2hsanNMb2FkZXIoKTogT2JzZXJ2YWJsZTxhbnk+IHtcclxuICAgIGlmICh0aGlzLl9kb2N1bWVudC5kZWZhdWx0Vmlldy5obGpzKSB7XHJcbiAgICAgIHJldHVybiB0aGlzLl9pbml0SExKUygpO1xyXG4gICAgfSBlbHNlIHtcclxuICAgICAgdGhpcy5fdGhlbWVMb2FkZXIoKS5zdWJzY3JpYmUoKTtcclxuICAgICAgcmV0dXJuIHRoaXMuX2xvYWRTY3JpcHQoKTtcclxuICAgIH1cclxuICB9XHJcblxyXG4gIC8qKlxyXG4gICAqIExvYWQgaGxqcyBzY3JpcHRcclxuICAgKi9cclxuICBwcml2YXRlIF9sb2FkU2NyaXB0KCk6IE9ic2VydmFibGU8YW55PiB7XHJcbiAgICBjb25zdCBwcm9taXNlID0gbmV3IFByb21pc2UoKHJlc29sdmUpID0+IHtcclxuICAgICAgY29uc3Qgc2NyaXB0ID0gdGhpcy5fZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnc2NyaXB0Jyk7XHJcbiAgICAgIHNjcmlwdC5hc3luYyA9IHRydWU7XHJcbiAgICAgIHNjcmlwdC50eXBlID0gJ3RleHQvamF2YXNjcmlwdCc7XHJcbiAgICAgIHNjcmlwdC5vbmxvYWQgPSByZXNvbHZlO1xyXG4gICAgICBzY3JpcHQuc3JjID0gYCR7dGhpcy5vcHRpb25zLnBhdGh9L2hpZ2hsaWdodC5wYWNrLmpzYDtcclxuICAgICAgdGhpcy5fZG9jdW1lbnQuaGVhZC5hcHBlbmRDaGlsZChzY3JpcHQpO1xyXG4gICAgfSk7XHJcbiAgICByZXR1cm4gZnJvbShwcm9taXNlKS5waXBlKFxyXG4gICAgICBzd2l0Y2hNYXAoKCkgPT4gdGhpcy5faW5pdEhMSlMoKSlcclxuICAgICk7XHJcbiAgfVxyXG5cclxuICAvKipcclxuICAgKiBMb2FkIGhsanMgdGhlbWVcclxuICAgKi9cclxuICBwcml2YXRlIF90aGVtZUxvYWRlcigpOiBPYnNlcnZhYmxlPGFueT4ge1xyXG4gICAgY29uc3QgcHJvbWlzZSA9IG5ldyBQcm9taXNlKChyZXNvbHZlKSA9PiB7XHJcbiAgICAgIGNvbnN0IHN0eWxlID0gdGhpcy5fZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnbGluaycpO1xyXG4gICAgICBzdHlsZS5yZWwgPSAnc3R5bGVzaGVldCc7XHJcbiAgICAgIHN0eWxlLnR5cGUgPSAndGV4dC9jc3MnO1xyXG4gICAgICBzdHlsZS5vbmxvYWQgPSByZXNvbHZlO1xyXG4gICAgICBzdHlsZS5ocmVmID0gYCR7dGhpcy5vcHRpb25zLnBhdGh9L3N0eWxlcy8ke3RoaXMub3B0aW9ucy50aGVtZX0uY3NzYDtcclxuICAgICAgdGhpcy5fZG9jdW1lbnQuaGVhZC5hcHBlbmRDaGlsZChzdHlsZSk7XHJcbiAgICB9KTtcclxuICAgIHJldHVybiBmcm9tKHByb21pc2UpO1xyXG4gIH1cclxuXHJcbiAgLyoqXHJcbiAgICogSW5pdGlhbGl6ZSBobGpzIG9uIGxvYWRcclxuICAgKi9cclxuICBwcml2YXRlIF9pbml0SExKUygpIHtcclxuICAgIHJldHVybiBvZih7fSkucGlwZShcclxuICAgICAgdGFwKCgpID0+IHtcclxuICAgICAgICB0aGlzLmhsanMgPSB0aGlzLl9kb2N1bWVudC5kZWZhdWx0Vmlldy5obGpzO1xyXG4gICAgICAgIHRoaXMuaGxqcy5jb25maWd1cmUodGhpcy5vcHRpb25zLmNvbmZpZyk7XHJcbiAgICAgICAgdGhpcy5faXNSZWFkeSQubmV4dCh0cnVlKTtcclxuICAgICAgfSlcclxuICAgICk7XHJcbiAgfVxyXG59XHJcbiIsImltcG9ydCB7IERpcmVjdGl2ZSwgRWxlbWVudFJlZiwgUmVuZGVyZXIyLCBPbkRlc3Ryb3ksIElucHV0LCBPdXRwdXQsIEV2ZW50RW1pdHRlciwgT25Jbml0IH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcbmltcG9ydCB7IGZyb20gfSBmcm9tICdyeGpzJztcclxuaW1wb3J0IHsgbWFwLCB0YWtlLCBmaWx0ZXIgfSBmcm9tICdyeGpzL29wZXJhdG9ycyc7XHJcbmltcG9ydCB7IEhpZ2hsaWdodEpTIH0gZnJvbSAnLi9oaWdobGlnaHQuc2VydmljZSc7XHJcbmltcG9ydCB7IEhpZ2hsaWdodFJlc3VsdCB9IGZyb20gJy4vaGlnaGxpZ2h0Lm1vZGVsJztcclxuXHJcbi8qKiBUaGVyZSBhcmUgMiB3YXlzIHRvIGhpZ2xpZ2h0IGEgY29kZVxyXG4gKiAgMSAtIHVzaW5nIHRoZSBbY29kZV0gaW5wdXQgKGRlZmF1bHQpIDxjb2RlIGhpZ2hsaWdodCBbY29kZV09XCJ5b3VyQ29kZVwiPjwvY29kZT5cclxuICogIDIgLSB1c2luZyBlbGVtZW50IHRleHQgY29udGVudCA8Y29kZT4ge3t5b3VyQ29kZX19IDwvY29kZT5cclxuICovXHJcblxyXG5ARGlyZWN0aXZlKHtcclxuICBzZWxlY3RvcjogJ1toaWdobGlnaHRdJ1xyXG59KVxyXG5leHBvcnQgY2xhc3MgSGlnaGxpZ2h0RGlyZWN0aXZlIGltcGxlbWVudHMgT25Jbml0LCBPbkRlc3Ryb3kge1xyXG5cclxuICBlbDogSFRNTEVsZW1lbnQ7XHJcbiAgZG9tT2JzOiBNdXRhdGlvbk9ic2VydmVyO1xyXG4gIGNvZGU6IHN0cmluZztcclxuICBASW5wdXQoKSBoaWdobGlnaHQ6IHN0cmluZztcclxuICBASW5wdXQoKSBsYW5ndWFnZTogc3RyaW5nW107XHJcbiAgQElucHV0KCdjb2RlJylcclxuICBzZXQgc2V0Q29kZShjb2RlOiBzdHJpbmcpIHtcclxuICAgIHRoaXMuY29kZSA9IGNvZGU7XHJcbiAgICB0aGlzLmhsanMuaXNSZWFkeS5zdWJzY3JpYmUoKCkgPT4gdGhpcy5oaWdobGlnaHRFbGVtZW50KHRoaXMuZWwsIGNvZGUpKTtcclxuICB9XHJcblxyXG4gIEBPdXRwdXQoKSBoaWdobGlnaHRlZCA9IG5ldyBFdmVudEVtaXR0ZXI8SGlnaGxpZ2h0UmVzdWx0PigpO1xyXG5cclxuICBjb25zdHJ1Y3RvcihlbDogRWxlbWVudFJlZiwgcHJpdmF0ZSByZW5kZXJlcjogUmVuZGVyZXIyLCBwcml2YXRlIGhsanM6IEhpZ2hsaWdodEpTKSB7XHJcbiAgICB0aGlzLmVsID0gZWwubmF0aXZlRWxlbWVudDtcclxuICB9XHJcblxyXG4gIG5nT25Jbml0KCkge1xyXG5cclxuICAgIC8qKiBBY2l0dmF0ZSBNdXRhdGlvbk9ic2VydmVyIGlmIGBhdXRvYCBvcHRpb24gaXMgdHJ1ZSBhbmQgYFtjb2RlXWAgaW5wdXQgaXMgbm90IHVzZWRcclxuICAgICAqIFRoaXMgd2lsbCBoaWdobGlnaHQgdXNpbmcgdGhlIHRleHQgY29udGVudCAqL1xyXG4gICAgaWYgKCF0aGlzLmNvZGUgJiYgdGhpcy5obGpzLm9wdGlvbnMuYXV0bykge1xyXG5cclxuICAgICAgdGhpcy5obGpzLmlzUmVhZHkuc3Vic2NyaWJlKCgpID0+IHtcclxuICAgICAgICB0aGlzLmhpZ2hsaWdodFRleHRDb250ZW50KCk7XHJcblxyXG4gICAgICAgIC8qKiBIaWdobGlnaHQgd2hlbiB0ZXh0IGNvbnRlbnQgY2hhbmdlcyAqL1xyXG4gICAgICAgIHRoaXMuZG9tT2JzID0gbmV3IE11dGF0aW9uT2JzZXJ2ZXIoKCkgPT4gdGhpcy5oaWdobGlnaHRUZXh0Q29udGVudCgpKTtcclxuICAgICAgICB0aGlzLmRvbU9icy5vYnNlcnZlKHRoaXMuZWwsIHsgY2hpbGRMaXN0OiB0cnVlLCBzdWJ0cmVlOiB0cnVlIH0pO1xyXG4gICAgICB9KTtcclxuICAgIH1cclxuICB9XHJcblxyXG4gIC8qKiBIaWdobGlnaHQgdXNpbmcgZWxlbWVudCB0ZXh0IGNvbnRlbnQgKi9cclxuICBoaWdobGlnaHRUZXh0Q29udGVudCgpIHtcclxuICAgIGlmICghdGhpcy5oaWdobGlnaHQpIHtcclxuICAgICAgaWYgKHRoaXMuZWwudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSAnY29kZScpIHtcclxuICAgICAgICB0aGlzLmhpZ2hsaWdodEVsZW1lbnQodGhpcy5lbCwgdGhpcy5lbC5pbm5lclRleHQudHJpbSgpKTtcclxuICAgICAgfSBlbHNlIHtcclxuICAgICAgICBjb25zb2xlLndhcm4oYFtIaWdobGlnaHREaXJlY3RpdmVdOiBVc2UgJ2hpZ2hsaWdodCcgb24gPGNvZGU+IGVsZW1lbnQgb25seWApO1xyXG4gICAgICB9XHJcbiAgICB9IGVsc2UgaWYgKHRoaXMuaGlnaGxpZ2h0ID09PSAnYWxsJykge1xyXG4gICAgICB0aGlzLmhpZ2hsaWdodENoaWxkcmVuKHRoaXMuZWwsICdwcmUgY29kZScpO1xyXG4gICAgfSBlbHNlIHtcclxuICAgICAgdGhpcy5oaWdobGlnaHRDaGlsZHJlbih0aGlzLmVsLCB0aGlzLmhpZ2hsaWdodCk7XHJcbiAgICB9XHJcbiAgfVxyXG5cclxuICAvKiogSGlnaGxpZ2h0IGEgY29kZSBibG9jayAqL1xyXG4gIGhpZ2hsaWdodEVsZW1lbnQoZWw6IEhUTUxFbGVtZW50LCBjb2RlOiBzdHJpbmcpIHtcclxuXHJcbiAgICBjb25zdCByZXM6IEhpZ2hsaWdodFJlc3VsdCA9IHRoaXMuaGxqcy5oaWdobGlnaHRBdXRvKGNvZGUsIHRoaXMubGFuZ3VhZ2UpO1xyXG4gICAgaWYgKHJlcy52YWx1ZSAhPT0gZWwuaW5uZXJIVE1MKSB7XHJcbiAgICAgIHRoaXMucmVuZGVyZXIuYWRkQ2xhc3MoZWwsICdobGpzJyk7XHJcbiAgICAgIHRoaXMucmVuZGVyZXIuc2V0UHJvcGVydHkoZWwsICdpbm5lckhUTUwnLCByZXMudmFsdWUpO1xyXG4gICAgICB0aGlzLmhpZ2hsaWdodGVkLmVtaXQocmVzKTtcclxuICAgIH1cclxuICB9XHJcblxyXG4gIC8qKiBIaWdobGlnaHQgbXVsdGlwbGUgY29kZSBibG9ja3MgKi9cclxuICBoaWdobGlnaHRDaGlsZHJlbihlbDogSFRNTEVsZW1lbnQsIHNlbGVjdG9yOiBzdHJpbmcpIHtcclxuXHJcbiAgICBjb25zdCBjb2RlRWxlbWVudHMgPSBlbC5xdWVyeVNlbGVjdG9yQWxsKHNlbGVjdG9yKTtcclxuXHJcbiAgICAvKiogaGlnaGxpZ2h0IGNoaWxkcmVuIHdpdGggdGhlIHNhbWUgc2VsZWN0b3IgKi9cclxuICAgIGZyb20oY29kZUVsZW1lbnRzKS5waXBlKFxyXG4gICAgICBmaWx0ZXIoKGNvZGU6IEhUTUxFbGVtZW50KSA9PiBjb2RlLmNoaWxkTm9kZXMubGVuZ3RoID09PSAxICYmIGNvZGUuY2hpbGROb2Rlc1swXS5ub2RlTmFtZSA9PT0gJyN0ZXh0JyksXHJcbiAgICAgIG1hcCgoY29kZUVsZW1lbnQ6IEhUTUxFbGVtZW50KSA9PiB0aGlzLmhpZ2hsaWdodEVsZW1lbnQoY29kZUVsZW1lbnQsIGNvZGVFbGVtZW50LmlubmVyVGV4dC50cmltKCkpKSxcclxuICAgICAgdGFrZSgxKVxyXG4gICAgKS5zdWJzY3JpYmUoKTtcclxuICB9XHJcblxyXG4gIG5nT25EZXN0cm95KCkge1xyXG4gICAgLyoqIERpc2Nvbm5lY3QgTXV0YXRpb25PYnNlcnZlciAqL1xyXG4gICAgaWYgKCF0aGlzLmNvZGUgJiYgdGhpcy5obGpzLm9wdGlvbnMuYXV0bykge1xyXG4gICAgICB0aGlzLmRvbU9icy5kaXNjb25uZWN0KCk7XHJcbiAgICB9XHJcbiAgfVxyXG59XHJcbiIsImltcG9ydCB7IE1vZHVsZVdpdGhQcm92aWRlcnMsIE5nTW9kdWxlIH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcbmltcG9ydCB7IEhpZ2hsaWdodERpcmVjdGl2ZSB9IGZyb20gJy4vaGlnaGxpZ2h0LmRpcmVjdGl2ZSc7XHJcbmltcG9ydCB7IEhpZ2hsaWdodE9wdGlvbnMgfSBmcm9tICcuL2hpZ2hsaWdodC5tb2RlbCc7XHJcbmltcG9ydCB7IE9QVElPTlMgfSBmcm9tICcuL2hpZ2hsaWdodC50b2tlbic7XHJcblxyXG5ATmdNb2R1bGUoe1xyXG4gIGRlY2xhcmF0aW9uczogW0hpZ2hsaWdodERpcmVjdGl2ZV0sXHJcbiAgZXhwb3J0czogW0hpZ2hsaWdodERpcmVjdGl2ZV1cclxufSlcclxuZXhwb3J0IGNsYXNzIEhpZ2hsaWdodE1vZHVsZSB7XHJcbiAgc3RhdGljIGZvclJvb3Qob3B0aW9ucz86IEhpZ2hsaWdodE9wdGlvbnMpOiBNb2R1bGVXaXRoUHJvdmlkZXJzIHtcclxuICAgIHJldHVybiB7XHJcbiAgICAgIG5nTW9kdWxlOiBIaWdobGlnaHRNb2R1bGUsXHJcbiAgICAgIHByb3ZpZGVyczogW1xyXG4gICAgICAgIHtwcm92aWRlOiBPUFRJT05TLCB1c2VWYWx1ZTogb3B0aW9uc31cclxuICAgICAgXVxyXG4gICAgfTtcclxuICB9XHJcbn1cclxuIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7QUFBQTtBQUdBLElBQWEsT0FBTyxHQUFHLElBQUksY0FBYyxDQUFtQixTQUFTLENBQUM7Ozs7Ozs7SUMyQnBFLHFCQUF5QyxPQUF5QixFQUM1QixTQUFjO1FBQWQsY0FBUyxHQUFULFNBQVMsQ0FBSztRQWpCcEQsWUFBTyxHQUFxQjtZQUMxQixLQUFLLEVBQUUsUUFBUTtZQUNmLElBQUksRUFBRSxpQkFBaUI7WUFDdkIsSUFBSSxFQUFFLElBQUk7U0FDWCxDQUFDO1FBRU0sY0FBUyxHQUFHLElBQUksZUFBZSxDQUFDLEtBQUssQ0FBQyxDQUFDO1FBWTdDLElBQUksQ0FBQyxPQUFPLGdCQUFRLElBQUksQ0FBQyxPQUFPLEVBQUssT0FBTyxDQUFFLENBQUM7UUFDL0MsSUFBSSxDQUFDLFdBQVcsRUFBRSxDQUFDLFNBQVMsRUFBRSxDQUFDO0tBQ2hDO0lBWEQsc0JBQUksZ0NBQU87Ozs7Ozs7UUFBWDtZQUNFLE9BQU8sSUFBSSxDQUFDLFNBQVMsQ0FBQyxJQUFJLENBQ3hCLE1BQU0sQ0FBQyxVQUFBLE9B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

/***/ }),

/***/ "./src/app/content/pages/builder/builder.component.html":
/*!**************************************************************!*\
  !*** ./src/app/content/pages/builder/builder.component.html ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<m-notice [icon]=\"'flaticon-cogwheel-2 m--font-brand'\">\r\n\t<p>\r\n\t\tThe layout builder helps to configure the layout with preferred options and preview it in real time. The configured layout options will be saved until you change or reset them. To use the layout builder choose the layout options and click the\r\n\t\t<code>Preview</code> button to preview the changes and click the\r\n\t\t<code>Export</code> to export your changes with following options:\r\n\t</p>\r\n</m-notice>\r\n\r\n<!--begin::Portlet-->\r\n<div class=\"m-portlet m-portlet--tabs\">\r\n\t<!--begin::Form-->\r\n\t<form class=\"m-form m-form--label-align-right m-form--fit\" novalidate #builderForm=\"ngForm\" (ngSubmit)=\"submitPreview(builderForm)\">\r\n\t\t<div class=\"m-portlet__body\">\r\n\r\n\t\t\t<mat-tab-group>\r\n\t\t\t\t<mat-tab label=\"Page\">\r\n\t\t\t\t\t<div class=\"m-portlet__body\">\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Layout Type:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[self][layout]\" [(ngModel)]=\"model.self.layout\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"fluid\">Fluid</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"boxed\">Boxed</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"boxed\">Wide</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<span class=\"m-form__help\">Select page layout type</span>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<!--<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Page Loader:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[loader][type]\" [(ngModel)]=\"model.loader.type\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"\">Disabled</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"default\">Default</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"spinner-message\">Spinner Message</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Select page loading indicator</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>-->\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Content Skin:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[content][skin]\" [(ngModel)]=\"model.content.skin\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"light\">White</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"light2\">Light Grey</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<span class=\"m-form__help\">Please select content skin</span>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</mat-tab>\r\n\t\t\t\t<mat-tab label=\"Header\">\r\n\t\t\t\t\t<div class=\"m-portlet__body\">\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Desktop Fixed Header:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[header][self][fixed][desktop]\" value=\"true\" [(ngModel)]=\"model.header.self.fixed.desktop\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Enable fixed header for desktop mode</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Mobile Fixed Header:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[header][self][fixed][mobile]\" value=\"true\" [(ngModel)]=\"model.header.self.fixed.mobile\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Enable fixed header for mobile mode</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\r\n\t\t\t\t\t\t<div class=\"m-separator m-separator--dashed\"></div>\r\n\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Display Header Menu:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[menu][header][display]\" value=\"true\" [(ngModel)]=\"model.menu.header.display\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Display header menu</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Dropdown Skin(Desktop):</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[menu][header][desktop][submenu][skin]\" [(ngModel)]=\"model.menu.header.desktop.submenu.skin\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"light\">Light</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"dark\">Dark</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<span class=\"m-form__help\">Please select header menu dropdown skin</span>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Display Submenu Arrow(Desktop):</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[menu][header][desktop][submenu][arrow]\" value=\"true\" [(ngModel)]=\"model.menu.header.desktop.submenu.arrow\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Display header menu dropdown arrows on desktop mode</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\r\n\t\t\t\t\t\t<div class=\"m-separator m-separator--dashed\"></div>\r\n\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Search Type:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[header][search][type]\" [(ngModel)]=\"model.header.search.type\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"search-default\">Expandable Search</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"search-dropdown\">Dropdown Search</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<span class=\"m-form__help\">Please header search type</span>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Dropdown Skin:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[header][search][dropdown][skin]\" [(ngModel)]=\"model.header.search.dropdown.skin\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"light\">Light</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"dark\">Dark</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<span class=\"m-form__help\">Please select search results dropdown skin for dropdown search type</span>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</mat-tab>\r\n\t\t\t\t<mat-tab label=\"Left Aside\">\r\n\t\t\t\t\t<div class=\"m-portlet__body\">\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row m--hide\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Display Aside:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[aside][left][display]\" value=\"true\" [(ngModel)]=\"model.aside.left.display\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Display left aside</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Aside Skin:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[aside][left][skin]\" [(ngModel)]=\"model.aside.left.skin\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"dark\">Dark</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"light\">Light</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Set left aside skin</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Fixed Aside:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[aside][left][fixed]\" value=\"true\" [(ngModel)]=\"model.aside.left.fixed\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Set fixed aside layout</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Allow Aside Minimizing:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[aside][left][minimize][toggle]\" value=\"true\" [(ngModel)]=\"model.aside.left.minimize.toggle\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Allow aside minimizing</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Default Minimized Aside:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[aside][left][minimize][default]\" value=\"true\" [(ngModel)]=\"model.aside.left.minimize.default\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Set aside minimized by default</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\r\n\t\t\t\t\t\t<div class=\"m-separator m-separator--dashed\"></div>\r\n\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Submenu Toggle:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[menu][aside][desktop_and_mobile][submenu][accordion]\" [(ngModel)]=\"model.menu.aside.desktop_and_mobile.submenu.accordion\">\r\n\t\t\t\t\t\t\t\t\t<option [ngValue]=\"false\">Dropdown</option>\r\n\t\t\t\t\t\t\t\t\t<option [ngValue]=\"true\">Accordion</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Select submenu toggle mode</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Dropdown Submenu Skin:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<select class=\"form-control\" name=\"builder[menu][aside][submenu][skin]\" [(ngModel)]=\"model.menu.aside.desktop_and_mobile.submenu.skin\">\r\n\t\t\t\t\t\t\t\t\t<option value=\"inherit\">Inherit</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"dark\">Dark</option>\r\n\t\t\t\t\t\t\t\t\t<option value=\"light\">Light</option>\r\n\t\t\t\t\t\t\t\t</select>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Select submenu skin. If\r\n\t\t\t\t\t\t\t\t\t<code>Inherit</code> is selected it will use the left aside's skin.</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Dropdown Submenu Arrow:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[menu][aside][submenu][dropdown][arrow]\" value=\"true\" [(ngModel)]=\"model.menu.aside.desktop_and_mobile.submenu.dropdown.arrow\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Enable dropdown submenu arrow</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</mat-tab>\r\n\t\t\t\t<mat-tab label=\"Right Aside\">\r\n\t\t\t\t\t<div class=\"m-portlet__body\">\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Display Right Aside:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[aside][right][display]\" value=\"true\" [(ngModel)]=\"model.aside.right.display\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Display right aside</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</mat-tab>\r\n\t\t\t\t<mat-tab label=\"Footer\">\r\n\t\t\t\t\t<div class=\"m-portlet__body\">\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Fixed Footer:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[footer][fixed]\" value=\"true\" [(ngModel)]=\"model.footer.fixed\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Set fixed header</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"form-group m-form__group row\">\r\n\t\t\t\t\t\t\t<label class=\"col-lg-4 col-form-label\">Push Footer:</label>\r\n\t\t\t\t\t\t\t<div class=\"col-lg-8 col-xl-4\">\r\n\t\t\t\t\t\t\t\t<span class=\"m-switch m-switch--icon-check\">\r\n\t\t\t\t\t\t\t\t\t<label>\r\n\t\t\t\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"builder[aside][left][push_footer]\" value=\"true\" [(ngModel)]=\"model.aside.left.push_footer\" />\r\n\t\t\t\t\t\t\t\t\t\t<span></span>\r\n\t\t\t\t\t\t\t\t\t</label>\r\n\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t<div class=\"m-form__help\">Push footer to the right</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</mat-tab>\r\n\t\t\t</mat-tab-group>\r\n\r\n\t\t</div>\r\n\r\n\t\t<div class=\"m-portlet__foot m-portlet__foot--fit\">\r\n\t\t\t<div class=\"m-form__actions\">\r\n\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t<div class=\"col-lg-4\"></div>\r\n\t\t\t\t\t<div class=\"col-lg-8 \">\r\n\t\t\t\t\t\t<button type=\"submit\" name=\"builder_submit\" data-demo=\"\" class=\"btn btn-primary m-btn m-btn--icon m-btn--wide m-btn--air m-btn--custom\">\r\n\t\t\t\t\t\t\t<span>\r\n\t\t\t\t\t\t\t\t<i class=\"la la-eye\"></i>\r\n\t\t\t\t\t\t\t\t<span>Preview</span>\r\n\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t</button>\r\n\t\t\t\t\t\t&nbsp;\r\n\t\t\t\t\t\t<button type=\"submit\" name=\"builder_submit\" data-demo=\"\" class=\"btn btn-accent m-btn m-btn--icon m-btn--wide m-btn--air m-btn--custom\">\r\n\t\t\t\t\t\t\t<span>\r\n\t\t\t\t\t\t\t\t<i class=\"la la-cog\"></i>\r\n\t\t\t\t\t\t\t\t<span>Export</span>\r\n\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t</button>\r\n\t\t\t\t\t\t&nbsp;\r\n\t\t\t\t\t\t<button type=\"submit\" (click)=\"resetPreview($event)\" name=\"builder_reset\" class=\"btn btn-secondary m-btn m-btn--icon m-btn--wide m-btn--air m-btn--custom\">\r\n\t\t\t\t\t\t\t<span>\r\n\t\t\t\t\t\t\t\t<i class=\"la la-recycle\"></i>\r\n\t\t\t\t\t\t\t\t<span>Reset</span>\r\n\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t</button>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</form>\r\n</div>\r\n\r\n<m-portlet>\r\n\t<ng-container mPortletHeadTitle>\r\n\t\t<div class=\"m-portlet__head-title\">\r\n\t\t\t<h3 class=\"m-portlet__head-text\">\r\n\t\t\t\tGenerated Config <small>can be used for layout config in <code>/src/app/config/layout.ts</code></small>\r\n\t\t\t</h3>\r\n\t\t</div>\r\n\t</ng-container>\r\n\t<ng-container mPortletBody>\r\n\t\t<div perfectScrollbar style=\"max-height: 400px\">\r\n\t\t</div>\r\n\t\t<pre><code highlight class=\"json\">{{model|json}}</code></pre>\r\n\t</ng-container>\r\n</m-portlet>\r\n"

/***/ }),

/***/ "./src/app/content/pages/builder/builder.component.ts":
/*!************************************************************!*\
  !*** ./src/app/content/pages/builder/builder.component.ts ***!
  \************************************************************/
/*! exports provided: BuilderComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuilderComponent", function() { return BuilderComponent; });
/* harmony import */ var _core_services_layout_config_storage_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../core/services/layout-config-storage.service */ "./src/app/core/services/layout-config-storage.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _core_services_layout_config_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../core/services/layout-config.service */ "./src/app/core/services/layout-config.service.ts");
/* harmony import */ var _core_services_class_init_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../core/services/class-init.service */ "./src/app/core/services/class-init.service.ts");
/* harmony import */ var _config_layout__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../config/layout */ "./src/app/config/layout.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var BuilderComponent = /** @class */ (function () {
    function BuilderComponent(layoutConfigService, classInitService, layoutConfigStorageService) {
        var _this = this;
        this.layoutConfigService = layoutConfigService;
        this.classInitService = classInitService;
        this.layoutConfigStorageService = layoutConfigStorageService;
        this.layoutConfigService.onLayoutConfigUpdated$.subscribe(function (config) {
            _this.model = config.config;
        });
    }
    BuilderComponent.prototype.ngOnInit = function () { };
    BuilderComponent.prototype.submitPreview = function (form) {
        this.layoutConfigService.setModel(new _config_layout__WEBPACK_IMPORTED_MODULE_5__["LayoutConfig"](this.model));
    };
    BuilderComponent.prototype.resetPreview = function (event) {
        event.preventDefault();
        this.layoutConfigStorageService.resetConfig();
        location.reload();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Input"])(),
        __metadata("design:type", Object)
    ], BuilderComponent.prototype, "model", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ViewChild"])('builderForm'),
        __metadata("design:type", _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgForm"])
    ], BuilderComponent.prototype, "form", void 0);
    BuilderComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'm-builder',
            template: __webpack_require__(/*! ./builder.component.html */ "./src/app/content/pages/builder/builder.component.html"),
            changeDetection: _angular_core__WEBPACK_IMPORTED_MODULE_1__["ChangeDetectionStrategy"].OnPush
        }),
        __metadata("design:paramtypes", [_core_services_layout_config_service__WEBPACK_IMPORTED_MODULE_3__["LayoutConfigService"],
            _core_services_class_init_service__WEBPACK_IMPORTED_MODULE_4__["ClassInitService"],
            _core_services_layout_config_storage_service__WEBPACK_IMPORTED_MODULE_0__["LayoutConfigStorageService"]])
    ], BuilderComponent);
    return BuilderComponent;
}());



/***/ }),

/***/ "./src/app/content/pages/builder/builder.module.ts":
/*!*********************************************************!*\
  !*** ./src/app/content/pages/builder/builder.module.ts ***!
  \*********************************************************/
/*! exports provided: BuilderModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuilderModule", function() { return BuilderModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _layout_layout_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../layout/layout.module */ "./src/app/content/layout/layout.module.ts");
/* harmony import */ var _builder_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./builder.component */ "./src/app/content/pages/builder/builder.component.ts");
/* harmony import */ var _partials_partials_module__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../partials/partials.module */ "./src/app/content/partials/partials.module.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @ng-bootstrap/ng-bootstrap */ "./node_modules/@ng-bootstrap/ng-bootstrap/index.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var ngx_highlightjs__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ngx-highlightjs */ "./node_modules/ngx-highlightjs/fesm5/ngx-highlightjs.js");
/* harmony import */ var ngx_perfect_scrollbar__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ngx-perfect-scrollbar */ "./node_modules/ngx-perfect-scrollbar/dist/ngx-perfect-scrollbar.es5.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











var BuilderModule = /** @class */ (function () {
    function BuilderModule() {
    }
    BuilderModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _layout_layout_module__WEBPACK_IMPORTED_MODULE_3__["LayoutModule"],
                _partials_partials_module__WEBPACK_IMPORTED_MODULE_5__["PartialsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_6__["FormsModule"],
                _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_7__["NgbModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatTabsModule"],
                ngx_perfect_scrollbar__WEBPACK_IMPORTED_MODULE_10__["PerfectScrollbarModule"],
                ngx_highlightjs__WEBPACK_IMPORTED_MODULE_9__["HighlightModule"].forRoot({ theme: 'googlecode' }),
                _angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"].forChild([
                    {
                        path: '',
                        component: _builder_component__WEBPACK_IMPORTED_MODULE_4__["BuilderComponent"]
                    }
                ])
            ],
            providers: [],
            declarations: [_builder_component__WEBPACK_IMPORTED_MODULE_4__["BuilderComponent"]]
        })
    ], BuilderModule);
    return BuilderModule;
}());



/***/ })

}]);
//# sourceMappingURL=builder-builder-module.js.map