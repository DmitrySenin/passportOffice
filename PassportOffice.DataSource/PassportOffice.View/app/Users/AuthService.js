;(function() {
	'use strict';

	angular
		.module('main')
		.factory('AuthService', AuthService);

	AuthService.$inject = ['$http', '$q', 'URLService'];

	function AuthService($http, $q, URLService) {
		
		/**
		 * Creates authentication information object.
		 *
		 * @return {Object} Object which fields represent authentication state of current user.
		 */
		function AuthInfo() {
			this.isAuthenticated = false;
			this.userName = '';
			this.Set = function(isAuthenticated, userName) {
				this.isAuthenticated = isAuthenticated;
				this.userName = userName; 
			};
		}

		/**
		 * Set authentication setting to default values.
		 * @param {Object} authInfo Object represnts authentication state.
		 */
		AuthInfo.Reset = function(authInfo) {
			authInfo.Set(false, '');
		};

		var authInfo = new AuthInfo();

		/**
		 * Try to login user with passed credentials.
		 * 
		 * @param {String} username Registered name of user.
		 * @param {String} password Password of user.
		 *
		 * @return {Object} Promise that represents flow of request.
		 */
		function login(username, password) {
			var data = "grant_type=password&username=" + username + "&password=" + password;
			var headers = {
				'Content-Type': 'application/x-www-form-urlencoded'
			};
			var config = {
				headers : headers
			};
			var deferred = $q.defer();

			$http.post(URLService.BuildLoginURL(), data, config).success(function(response){

				authInfo.Set(true, username);

				deferred.resolve(response);

			}).error(function(error) {

				logout();
				deferred.reject(error);

			});

			return deferred.promise;
		};

		/**
		 * Sign out current user.
		 */
		function logout() {
			AuthInfo.Reset(authInfo);
		};

		// Factory desciption.
		return {
			get IsAuthenticated() {
				return authInfo.isAuthenticated;
			},
			get UserName() {
				return authInfo.userName;
			},
			Login: login,
			Logout: logout
		};
	}

})();