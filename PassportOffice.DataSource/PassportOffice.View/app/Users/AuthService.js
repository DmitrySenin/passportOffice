;(function() {
	'use strict';

	angular
		.module('main')
		.factory('AuthService', AuthService);

	AuthService.$inject = ['$http', '$q', 'URLService', 'AuthenticationInfoStorage'];

	function AuthService($http, $q, URLService, AuthenticationInfoStorage) {
		
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

				AuthenticationInfoStorage.SetInfo(true, username, response.access_token);

				deferred.resolve(response);

			}).error(function(error) {

				logout();
				deferred.reject(error);

			});

			return deferred.promise;
		};

		/**
		 * Sends request to verify current user for admninistrator and puts result to storage.
		 * 
		 * @return {Object} Promise that represents flow of request.
		 */
		function isAdmin() {
			
			if(AuthenticationInfoStorage.IsAuthenticated) {
				
				var deferred = $q.defer();

				$http.get(URLService.BuildIsAdminURL()).success(function(isAdmin) {
					AuthenticationInfoStorage.SetAdminMode(isAdmin);
					deferred.resolve();
				}).error(function(error) {
					deferred.reject();
				});

				return deferred.promise;

			} else {
				return $q.reject("User is not authenticated yet.");
			}
		}

		/**
		 * Sign out current user.
		 */
		function logout() {
			AuthenticationInfoStorage.ResetInfo();
		};

		// Factory desciption.
		return {
			Login: login,
			Logout: logout,
			IsAdmin : isAdmin
		};
	}

})();