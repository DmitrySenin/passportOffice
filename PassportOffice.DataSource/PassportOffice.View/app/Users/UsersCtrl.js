;(function() {
	'use strict';

	angular
		.module('main')
		.controller('UsersCtrl', UsersCtrl);

	UsersCtrl.$inject = ['AuthService', 'AuthenticationInfoStorage', 'EventNames', '$rootScope'];

	function UsersCtrl(AuthService, AuthenticationInfoStorage, EventNames, $rootScope) {
		var vm = this;

		catchAuthenticationFlag(vm);
		createCredentialsFields(vm);
		setAuthSuccessFlag(vm, true);

		/**
		 * Try to login user with entered credentials.
		 */
		vm.Login = function() {
			AuthService.Login(vm.username, vm.password).then(function(data) {
				catchAuthenticationFlag(vm);
				hideLoginForm();
				setAuthSuccessFlag(vm, true);

				AuthService.IsAdmin().then(function() {
					if(AuthenticationInfoStorage.IsAdmin) {
						alertAboutAdmin();
					}
				});
			}, 
			function(error) {
				catchAuthenticationFlag(vm);
				setAuthSuccessFlag(vm, false);
			});
		};

		/**
		 * Logout current user.
		 */
		vm.Logout = function() {
			AuthService.Logout();
			catchAuthenticationFlag(vm);
			alertAboutLogout();
		};

		/**
		 * Add variables to store credentials to scope.
		 * @param {Object} scope Object where credentials should be added.
		 */
		function createCredentialsFields(scope) {
			scope.username = '';
			scope.password = '';
		}

		/**
		 * Get flag of user authentication from service and set value to scope.
		 * @param {Object} scope Object where credentials should be added.
		 */
		function catchAuthenticationFlag(scope) {
			vm.IsAuthenticated = AuthenticationInfoStorage.IsAuthenticated;
		}

		/**
		 * Set login form invisible.
		 */
		function hideLoginForm() {
			$('.dropdown').removeClass('open');
		}

		/**
		 * Set value of flag that represent successful authentication.
		 * @param {Object} scope Object with flag.
		 * @param {Boolean} value New value of flag.
		 */	
		function setAuthSuccessFlag(scope, value) {
			scope.IsAuthSuccess = !!value;
		}

		/**
		 * Send notification about admin authentication.
		 */
		function alertAboutAdmin() {
			$rootScope.$emit(EventNames.AuthAdmin);
		}

		/**
		 * Send notification about user logout.
		 */
		function alertAboutLogout() {
			$rootScope.$emit(EventNames.Logout);
		}
	}

})();