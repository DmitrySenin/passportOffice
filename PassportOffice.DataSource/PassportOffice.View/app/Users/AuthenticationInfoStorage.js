;(function() {
	angular
		.module('main')
		.factory('AuthenticationInfoStorage', AuthenticationInfoStorage);

	function AuthenticationInfoStorage() {
		
		/**
		 * Creates authentication information object.
		 *
		 * @return {Object} Object which fields represent authentication state of current user.
		 */
		function AuthInfo() {
			var authInfo = this;
			
			authInfo.isAuthenticated = false;
			authInfo.userName = '';
			authInfo.token = undefined;
			
			authInfo.Set = function(isAuthenticated, userName, token) {
				authInfo.isAuthenticated = isAuthenticated;
				authInfo.userName = userName; 
				authInfo.token = token;
			};

			/**
			 * Set authentication setting to default values.
			 */
			authInfo.Reset = function() {
				authInfo.Set(false, '', undefined);
			}
		}

		var authInfo = new AuthInfo();

		// Factory public interface.
		return {
			get IsAuthenticated() {
				return authInfo.isAuthenticated;
			},
			get UserName() {
				return authInfo.userName;
			},
			get AccessToken() {
				return authInfo.token;
			},
			SetInfo : authInfo.Set,
			ResetInfo : authInfo.Reset
		};
	}
})();