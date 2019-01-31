(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["history-history-module"],{

/***/ "./node_modules/guid-typescript/dist/guid.js":
/*!***************************************************!*\
  !*** ./node_modules/guid-typescript/dist/guid.js ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

exports.__esModule = true;
var Guid = /** @class */ (function () {
    function Guid(guid) {
        if (!guid) {
            throw new TypeError("Invalid argument; `value` has no value.");
        }
        this.value = Guid.EMPTY;
        if (guid && Guid.isGuid(guid)) {
            this.value = guid;
        }
    }
    Guid.isGuid = function (guid) {
        var value = guid.toString();
        return guid && (guid instanceof Guid || Guid.validator.test(value));
    };
    Guid.create = function () {
        return new Guid([Guid.gen(2), Guid.gen(1), Guid.gen(1), Guid.gen(1), Guid.gen(3)].join("-"));
    };
    Guid.createEmpty = function () {
        return new Guid("emptyguid");
    };
    Guid.parse = function (guid) {
        return new Guid(guid);
    };
    Guid.raw = function () {
        return [Guid.gen(2), Guid.gen(1), Guid.gen(1), Guid.gen(1), Guid.gen(3)].join("-");
    };
    Guid.gen = function (count) {
        var out = "";
        for (var i = 0; i < count; i++) {
            // tslint:disable-next-line:no-bitwise
            out += (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        return out;
    };
    Guid.prototype.equals = function (other) {
        // Comparing string `value` against provided `guid` will auto-call
        // toString on `guid` for comparison
        return Guid.isGuid(other) && this.value === other.toString();
    };
    Guid.prototype.isEmpty = function () {
        return this.value === Guid.EMPTY;
    };
    Guid.prototype.toString = function () {
        return this.value;
    };
    Guid.prototype.toJSON = function () {
        return {
            value: this.value
        };
    };
    Guid.validator = new RegExp("^[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}$", "i");
    Guid.EMPTY = "00000000-0000-0000-0000-000000000000";
    return Guid;
}());
exports.Guid = Guid;


/***/ }),

/***/ "./src/app/history/history.component.css":
/*!***********************************************!*\
  !*** ./src/app/history/history.component.css ***!
  \***********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhcHAvaGlzdG9yeS9oaXN0b3J5LmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/history/history.component.html":
/*!************************************************!*\
  !*** ./src/app/history/history.component.html ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div style=\"text-align:center\">\r\n    <div *ngFor=\"let match of matches\">\r\n        <div>\r\n            || Date of game - {{match.date | date:'yyyy-MM-dd'}}, time - {{match.date | date:'hh:mm'}} ||\r\n            <button (click)=\"getGameDetails(match.id)\">Get info</button>\r\n            <div *ngIf=\"match.id === currentMatch.game.id\">\r\n                <button (click)=\"closeDetails()\">Hide info</button>\r\n                <div>\r\n                    <div style=\"border:1px black\">\r\n                    </div>\r\n                    <div style=\"text-align:center\" *ngFor=\"let gamer of currentMatch.gamers\">\r\n                        Name - {{gamer.name}}<br>\r\n                        Gamer status - {{gamer.gamerStatus}}<br>\r\n                        <div *ngFor=\"let round of currentMatch.rounds\">\r\n                            <div *ngIf=\"round.userInGameId==gamer.id\">\r\n                                <img src=\"../../assets/{{round.suit}}-{{round.value}}.png\">\r\n                                Received in round {{round.roundNumber+1}}\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <button (click)=\"redirectToMain()\">Go to main</button>\r\n</div>"

/***/ }),

/***/ "./src/app/history/history.component.ts":
/*!**********************************************!*\
  !*** ./src/app/history/history.component.ts ***!
  \**********************************************/
/*! exports provided: HistoryComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HistoryComponent", function() { return HistoryComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_shared_historyservice__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/shared.historyservice */ "./src/app/shared/services/shared.historyservice.ts");
/* harmony import */ var guid_typescript__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! guid-typescript */ "./node_modules/guid-typescript/dist/guid.js");
/* harmony import */ var guid_typescript__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(guid_typescript__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");






var HistoryComponent = /** @class */ (function () {
    function HistoryComponent(http, router, historyService) {
        this.http = http;
        this.router = router;
        this.historyService = historyService;
        this.currentMatch = {
            game: {
                id: guid_typescript__WEBPACK_IMPORTED_MODULE_4__["Guid"].create(),
                countOfRounds: 0,
                status: 0,
                date: new Date(),
            },
            rounds: [],
            gamers: []
        };
        this.getHistory();
    }
    HistoryComponent.prototype.getHistory = function () {
        var _this = this;
        this.historyService.gethistory(this.http, "").subscribe(function (result) {
            _this.matches = result;
        }, function (error) { return console.error(error); });
    };
    HistoryComponent.prototype.getGameDetails = function (id) {
        var _this = this;
        this.historyService.getGameDetails(this.http, id).subscribe(function (result) {
            _this.currentMatch = result;
            console.log(result);
        }, function (error) { return console.error(error); });
    };
    HistoryComponent.prototype.closeDetails = function (id) {
        this.currentMatch = {
            game: {
                id: guid_typescript__WEBPACK_IMPORTED_MODULE_4__["Guid"].create(),
                countOfRounds: 0,
                status: 0,
                date: new Date(),
            },
            rounds: [],
            gamers: []
        };
    };
    HistoryComponent.prototype.redirectToMain = function () {
        this.router.navigate(['/']);
    };
    HistoryComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'history',
            template: __webpack_require__(/*! ./history.component.html */ "./src/app/history/history.component.html"),
            providers: [src_app_shared_services_shared_historyservice__WEBPACK_IMPORTED_MODULE_3__["HistoryService"]],
            styles: [__webpack_require__(/*! ./history.component.css */ "./src/app/history/history.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], _angular_router__WEBPACK_IMPORTED_MODULE_5__["Router"], src_app_shared_services_shared_historyservice__WEBPACK_IMPORTED_MODULE_3__["HistoryService"]])
    ], HistoryComponent);
    return HistoryComponent;
}());



/***/ }),

/***/ "./src/app/history/history.module.ts":
/*!*******************************************!*\
  !*** ./src/app/history/history.module.ts ***!
  \*******************************************/
/*! exports provided: HistoryModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HistoryModule", function() { return HistoryModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _history_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./history.component */ "./src/app/history/history.component.ts");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");





var routes = [
    { path: '', component: _history_component__WEBPACK_IMPORTED_MODULE_1__["HistoryComponent"] }
];
var HistoryModule = /** @class */ (function () {
    function HistoryModule() {
    }
    HistoryModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_3__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_2__["CommonModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterModule"].forChild(routes)
            ],
            declarations: [
                _history_component__WEBPACK_IMPORTED_MODULE_1__["HistoryComponent"]
            ]
        })
    ], HistoryModule);
    return HistoryModule;
}());



/***/ }),

/***/ "./src/app/shared/services/shared.historyservice.ts":
/*!**********************************************************!*\
  !*** ./src/app/shared/services/shared.historyservice.ts ***!
  \**********************************************************/
/*! exports provided: HistoryService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HistoryService", function() { return HistoryService; });
/* harmony import */ var src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! src/environments/environment.local */ "./src/environments/environment.local.ts");

var HistoryService = /** @class */ (function () {
    function HistoryService() {
    }
    HistoryService.prototype.gethistory = function (http, userName) {
        return http.get(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/History/GetAllGames');
    };
    ;
    HistoryService.prototype.getGameDetails = function (http, id) {
        return http.get(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/History/GetMatchById/' + id);
    };
    ;
    return HistoryService;
}());



/***/ })

}]);
//# sourceMappingURL=history-history-module.js.map