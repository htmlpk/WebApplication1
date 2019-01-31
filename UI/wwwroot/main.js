(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"./history/history.module": [
		"./src/app/history/history.module.ts",
		"history-history-module"
	]
};
function webpackAsyncContext(req) {
	var ids = map[req];
	if(!ids) {
		return Promise.resolve().then(function() {
			var e = new Error("Cannot find module '" + req + "'");
			e.code = 'MODULE_NOT_FOUND';
			throw e;
		});
	}
	return __webpack_require__.e(ids[1]).then(function() {
		var id = ids[0];
		return __webpack_require__(id);
	});
}
webpackAsyncContext.keys = function webpackAsyncContextKeys() {
	return Object.keys(map);
};
webpackAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";
module.exports = webpackAsyncContext;

/***/ }),

/***/ "./src/app/app.component.css":
/*!***********************************!*\
  !*** ./src/app/app.component.css ***!
  \***********************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhcHAvYXBwLmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<body>\n      <div class=\"container\">  \n          <router-outlet></router-outlet>\n    </div>\n  </body>\n  "

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");


var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.title = 'ClientApp';
    }
    AppComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-root',
            template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html"),
            styles: [__webpack_require__(/*! ./app.component.css */ "./src/app/app.component.css")]
        })
    ], AppComponent);
    return AppComponent;
}());



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./login/login.component */ "./src/app/login/login.component.ts");
/* harmony import */ var _error_error_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./error/error.component */ "./src/app/error/error.component.ts");
/* harmony import */ var _game_game_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./game/game.component */ "./src/app/game/game.component.ts");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_http__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/http */ "./node_modules/@angular/http/fesm5/http.js");
/* harmony import */ var _shared_services_shared_interceptor__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./shared/services/shared.interceptor */ "./src/app/shared/services/shared.interceptor.ts");
/* harmony import */ var src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/services/shared.gameservice */ "./src/app/shared/services/shared.gameservice.ts");













var routes = [
    { path: '', component: _login_login_component__WEBPACK_IMPORTED_MODULE_1__["LoginComponent"], pathMatch: 'full' },
    { path: 'game', component: _game_game_component__WEBPACK_IMPORTED_MODULE_3__["GameComponent"] },
    { path: 'history', loadChildren: './history/history.module#HistoryModule' },
    { path: 'error', component: _error_error_component__WEBPACK_IMPORTED_MODULE_2__["ErrorComponent"] }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_5__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"],
                _login_login_component__WEBPACK_IMPORTED_MODULE_1__["LoginComponent"],
                _game_game_component__WEBPACK_IMPORTED_MODULE_3__["GameComponent"],
                _error_error_component__WEBPACK_IMPORTED_MODULE_2__["ErrorComponent"]
            ],
            imports: [
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["FormsModule"],
                _angular_http__WEBPACK_IMPORTED_MODULE_10__["HttpModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_6__["HttpClientModule"],
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_4__["BrowserModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_8__["RouterModule"].forRoot(routes)
            ],
            providers: [src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_12__["GameService"], {
                    provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_6__["HTTP_INTERCEPTORS"],
                    useClass: _shared_services_shared_interceptor__WEBPACK_IMPORTED_MODULE_11__["ParamInterceptor"],
                    multi: true
                }],
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/app/error/error.component.css":
/*!*******************************************!*\
  !*** ./src/app/error/error.component.css ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhcHAvZXJyb3IvZXJyb3IuY29tcG9uZW50LmNzcyJ9 */"

/***/ }),

/***/ "./src/app/error/error.component.html":
/*!********************************************!*\
  !*** ./src/app/error/error.component.html ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "{{errormessage}}"

/***/ }),

/***/ "./src/app/error/error.component.ts":
/*!******************************************!*\
  !*** ./src/app/error/error.component.ts ***!
  \******************************************/
/*! exports provided: ErrorComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ErrorComponent", function() { return ErrorComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/shared.gameservice */ "./src/app/shared/services/shared.gameservice.ts");




var ErrorComponent = /** @class */ (function () {
    function ErrorComponent(http, gameService) {
        this.http = http;
        this.gameService = gameService;
        this.errormessage = "";
    }
    ErrorComponent.prototype.ngOnInit = function () {
        this.errormessage = localStorage.getItem('error');
    };
    ErrorComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'error',
            template: __webpack_require__(/*! ./error.component.html */ "./src/app/error/error.component.html"),
            providers: [src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_3__["GameService"]],
            styles: [__webpack_require__(/*! ./error.component.css */ "./src/app/error/error.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_3__["GameService"]])
    ], ErrorComponent);
    return ErrorComponent;
}());



/***/ }),

/***/ "./src/app/game/game.component.css":
/*!*****************************************!*\
  !*** ./src/app/game/game.component.css ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhcHAvZ2FtZS9nYW1lLmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/game/game.component.html":
/*!******************************************!*\
  !*** ./src/app/game/game.component.html ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\n  <div style=\"border:1px black\">\n    <div [ngSwitch]=\"match.game.status\">\n      <div *ngSwitchCase=2>In progress</div>\n      <div *ngSwitchCase=1>Finished</div>\n    </div>\n    <div style=\"border:1px black\">Date of game - {{match.game.date | date:'dd-MM-yyyy'}}, time - {{match.game.date |\n      date:'hh:mm'}} </div>\n  </div>\n  <div style=\"text-align:center\" *ngFor=\"let gamer of match.gamers\">\n    Name - {{gamer.name}}<br>\n    <div *ngIf=\"match.game.status==1||gamer.name.indexOf('Bot')!=0\">\n      Points - {{gamer.points}} points.<br>\n      <div [ngSwitch]=\"gamer.gamerStatus\">\n        <div *ngSwitchCase=1> Gamer status - Loser</div>\n        <div *ngSwitchCase=2> Gamer status - Winner</div>\n        <div *ngSwitchCase=3> Gamer status - In game</div>\n      </div>\n    </div>\n    <div>\n      <div *ngFor=\"let round of match.rounds\">\n        <div *ngIf=\"round.userInGameId==gamer.id\">\n          <img *ngIf=\"match.game.status==1\" src=\"../../assets/{{round.suit}}-{{round.value}}.png\">\n          <img *ngIf=\"(match.game.status!==1)&&(gamer.name.indexOf('Bot')!=0)\" src=\"../../assets/{{round.suit}}-{{round.value}}.png\">\n          <img *ngIf=\"(match.game.status!==1)&&(gamer.name.indexOf('Bot')==0)\" src=\"../../assets/untitled.png\">\n        </div>\n      </div>\n    </div>\n    <button *ngIf=\"(gamer.name.indexOf('Bot')!=0)&&(match.game.status!=1)\" (click)=\"nextRound()\">Get card</button>\n    <button *ngIf=\"(gamer.name.indexOf('Bot')!=0)&&(match.game.status!=1)\" (click)=\"stopGame()\">Enough!</button>\n  </div>\n</div>"

/***/ }),

/***/ "./src/app/game/game.component.ts":
/*!****************************************!*\
  !*** ./src/app/game/game.component.ts ***!
  \****************************************/
/*! exports provided: GameComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameComponent", function() { return GameComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var src_environments_environment_local__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/environments/environment.local */ "./src/environments/environment.local.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/shared.gameservice */ "./src/app/shared/services/shared.gameservice.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");






var GameComponent = /** @class */ (function () {
    function GameComponent(http, gameService, router) {
        this.http = http;
        this.gameService = gameService;
        this.router = router;
        this.match = {
            game: {
                id: null,
                countOfRounds: 0,
                status: 0,
                date: new Date(),
            },
            rounds: [],
            gamers: [],
        };
        this.cardUrl = src_environments_environment_local__WEBPACK_IMPORTED_MODULE_1__["environment"].imageUrl;
        this.username = "q";
    }
    GameComponent.prototype.ngOnInit = function () {
        this.getGame();
    };
    GameComponent.prototype.getGame = function () {
        var _this = this;
        this.gameService.getGame(this.http, this.username).subscribe(function (result) {
            _this.match = result;
            console.log(_this.match);
        }, function (error) { console.error(error); _this.router.navigate(['/error']); });
    };
    GameComponent.prototype.nextRound = function () {
        var _this = this;
        this.gameService.nextRound(this.http, this.username).subscribe(function (result) {
            _this.match = result;
        }, function (error) { console.error(error); _this.router.navigate(['/error']); });
    };
    GameComponent.prototype.stopGame = function () {
        var _this = this;
        this.gameService.stopGame(this.http, this.username).subscribe(function (result) {
            _this.match = result;
        }, function (error) { console.error(error); _this.router.navigate(['/error']); });
    };
    GameComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"])({
            selector: 'game',
            template: __webpack_require__(/*! ./game.component.html */ "./src/app/game/game.component.html"),
            providers: [src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_4__["GameService"]],
            styles: [__webpack_require__(/*! ./game.component.css */ "./src/app/game/game.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"], src_app_shared_services_shared_gameservice__WEBPACK_IMPORTED_MODULE_4__["GameService"], _angular_router__WEBPACK_IMPORTED_MODULE_5__["Router"]])
    ], GameComponent);
    return GameComponent;
}());



/***/ }),

/***/ "./src/app/login/login.component.css":
/*!*******************************************!*\
  !*** ./src/app/login/login.component.css ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhcHAvbG9naW4vbG9naW4uY29tcG9uZW50LmNzcyJ9 */"

/***/ }),

/***/ "./src/app/login/login.component.html":
/*!********************************************!*\
  !*** ./src/app/login/login.component.html ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Enter the name and choose the count of bots to start the game! </h2>\n<input [(ngModel)]=\"username\"  list=\"usernames\" #name=\"ngModel\" required>\n<datalist id=\"usernames\">\n    <option *ngFor=\"let user of users\" [ngValue]=\"user.userName\">{{user}}</option>\n</datalist>\n<div [hidden]=\"name.valid || name.untouched \" class=\"alert alert-danger\">\n  Enter the name\n</div><br><br>  \n<select [(ngModel)]=\"countofbots\"  required>\n    <option >1</option>\n    <option >2</option>\n    <option >3</option>\n    <option >4</option>\n    <option >5</option>\n</select>\n<h2>\n  <button [disabled]=\"name.invalid\" (click)=\"startGame()\">Start game</button>\n  <button [disabled]=\"name.invalid\" (click)=\"watchHistory()\">Watch game history</button>\n</h2>"

/***/ }),

/***/ "./src/app/login/login.component.ts":
/*!******************************************!*\
  !*** ./src/app/login/login.component.ts ***!
  \******************************************/
/*! exports provided: LoginComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginComponent", function() { return LoginComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_shared_loginservice__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/shared.loginservice */ "./src/app/shared/services/shared.loginservice.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");





var LoginComponent = /** @class */ (function () {
    function LoginComponent(http, router, loginservice) {
        this.http = http;
        this.router = router;
        this.loginservice = loginservice;
        this.countofbots = '1';
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.getUsersName();
    };
    LoginComponent.prototype.getUsersName = function () {
        var _this = this;
        this.loginservice.getUsersName(this.http).subscribe(function (result) {
            _this.users = result;
        }, function (error) {
            console.error(error);
            localStorage.setItem('error', JSON.stringify(error['error']));
            _this.router.navigate(['/error']);
        });
    };
    LoginComponent.prototype.login = function () {
        var _this = this;
        this.loginservice.login(this.http, this.username).subscribe(function (result) {
            localStorage.setItem('token', JSON.stringify(result));
        }, function (error) {
            console.error(error);
            localStorage.setItem('error', JSON.stringify(error['error']));
            _this.router.navigate(['/error']);
        });
    };
    LoginComponent.prototype.startGame = function () {
        var _this = this;
        this.login();
        this.loginservice.startGame(this.http, this.username, this.countofbots).subscribe(function (result) {
            _this.router.navigate(['/game']);
        }, function (error) {
            console.error(error);
            localStorage.setItem('error', JSON.stringify(error['error']));
            _this.router.navigate(['/error']);
        });
    };
    LoginComponent.prototype.watchHistory = function () {
        this.login();
        this.router.navigate(['/history']);
    };
    LoginComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'login',
            template: __webpack_require__(/*! ./login.component.html */ "./src/app/login/login.component.html"),
            providers: [src_app_shared_services_shared_loginservice__WEBPACK_IMPORTED_MODULE_3__["LoginService"]],
            styles: [__webpack_require__(/*! ./login.component.css */ "./src/app/login/login.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"], src_app_shared_services_shared_loginservice__WEBPACK_IMPORTED_MODULE_3__["LoginService"]])
    ], LoginComponent);
    return LoginComponent;
}());



/***/ }),

/***/ "./src/app/shared/models/StartGameModel.ts":
/*!*************************************************!*\
  !*** ./src/app/shared/models/StartGameModel.ts ***!
  \*************************************************/
/*! exports provided: StartGameModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StartGameModel", function() { return StartGameModel; });
var StartGameModel = /** @class */ (function () {
    function StartGameModel() {
    }
    return StartGameModel;
}());



/***/ }),

/***/ "./src/app/shared/services/shared.gameservice.ts":
/*!*******************************************************!*\
  !*** ./src/app/shared/services/shared.gameservice.ts ***!
  \*******************************************************/
/*! exports provided: GameService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameService", function() { return GameService; });
/* harmony import */ var src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! src/environments/environment.local */ "./src/environments/environment.local.ts");

var GameService = /** @class */ (function () {
    function GameService() {
    }
    GameService.prototype.getGame = function (http, userName) {
        return http.get(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/Game/GetLastMatch/' + userName);
    };
    GameService.prototype.nextRound = function (http, userName) {
        return http.put(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/Game/NextRound/' + userName, 1);
    };
    GameService.prototype.stopGame = function (http, userName) {
        return http.put(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/Game/NextRound/' + userName, 0);
    };
    return GameService;
}());



/***/ }),

/***/ "./src/app/shared/services/shared.interceptor.ts":
/*!*******************************************************!*\
  !*** ./src/app/shared/services/shared.interceptor.ts ***!
  \*******************************************************/
/*! exports provided: ParamInterceptor */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ParamInterceptor", function() { return ParamInterceptor; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");


var ParamInterceptor = /** @class */ (function () {
    function ParamInterceptor() {
    }
    ParamInterceptor.prototype.intercept = function (req, next) {
        if (!req.url.includes('api/Account/')) {
            var token = JSON.parse(localStorage.getItem('token'));
            var paramReq = req.clone({
                headers: req.headers.set('Authorization', 'bearer ' + token['access_token'])
            });
            return next.handle(paramReq);
        }
        return next.handle(req);
    };
    ParamInterceptor = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])()
    ], ParamInterceptor);
    return ParamInterceptor;
}());



/***/ }),

/***/ "./src/app/shared/services/shared.loginservice.ts":
/*!********************************************************!*\
  !*** ./src/app/shared/services/shared.loginservice.ts ***!
  \********************************************************/
/*! exports provided: LoginService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginService", function() { return LoginService; });
/* harmony import */ var src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! src/environments/environment.local */ "./src/environments/environment.local.ts");
/* harmony import */ var src_app_shared_models_StartGameModel__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/models/StartGameModel */ "./src/app/shared/models/StartGameModel.ts");


var LoginService = /** @class */ (function () {
    function LoginService() {
    }
    LoginService.prototype.getUsersName = function (http) {
        return http.get(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/Account/GetUserNames');
    };
    ;
    LoginService.prototype.login = function (http, username) {
        return http.get(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/Account/LogIn/' + username);
    };
    ;
    LoginService.prototype.startGame = function (http, username, countofbots) {
        var model = new src_app_shared_models_StartGameModel__WEBPACK_IMPORTED_MODULE_1__["StartGameModel"]();
        model.userName = username;
        model.countOfBots = parseInt(countofbots);
        return http.post(src_environments_environment_local__WEBPACK_IMPORTED_MODULE_0__["environment"].apiUrl + '/api/Account/StartGame', model);
    };
    return LoginService;
}());



/***/ }),

/***/ "./src/environments/environment.local.ts":
/*!***********************************************!*\
  !*** ./src/environments/environment.local.ts ***!
  \***********************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
var environment = {
    production: true,
    apiUrl: 'http://localhost:58669',
    imageUrl: 'src/Card/'
};


/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
var environment = {
    production: false
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.error(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! C:\Work\Test2\WebApplication1\UI\ClientApp\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map