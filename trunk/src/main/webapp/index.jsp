
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<html>
<head>
<title>index</title>
<link rel="stylesheet" href="./resources/themes/base/jquery.ui.all.css">
<link rel="stylesheet" href="./resources/css/common.css">
<script  type="text/javascript" src="./resources/js/jquery-1.8.0.min.js"></script>
<script  type="text/javascript" src="./resources/js/jquery-ui-1.8.23.custom.min.js"></script>
<script type="text/javascript">
	$(function() {		
		$('.left-nav a').click(function(ev) {
			//window.location.hash = "index.html";
			$("#mainframe").attr("src", this.href);
			$('.left-nav a.selected').removeClass('selected');
			$(this).addClass('selected');
			ev.preventDefault();
		});
	});
	</script>
</head>
<body>
<div  class="left-nav">
	<dl class="demos-nav">
		<dt>Menu</dt>
			<dd><a href="./flash/index.swf">flash</a></dd>
			<dd><a href="./accezz/index" target="mainframe">Accezz</a></dd>
			<dd><a href="./actionControl/index" target="mainframe">ActionControl</a></dd>
			<dd><a href="./activity/index" target="mainframe">Activity</a></dd>
			<dd><a href="./actCfg/index" target="mainframe">ActCfg</a></dd>
			<dd><a href="./attachment/index" target="mainframe">Attachment</a></dd>
			<dd><a href="./clinic/index" target="mainframe">Clinic</a></dd>
			<dd><a href="./conprocess/index" target="mainframe">Conprocess</a></dd>
			<dd><a href="./consultation/index" target="mainframe">Consultation</a></dd>
			<dd><a href="./consultationClinic/index" target="mainframe">ConsultationClinic</a></dd>
			<dd><a href="./consultationDoctor/index" target="mainframe">ConsultationDoctor</a></dd>
			<dd><a href="./cvDicmeta/index" target="mainframe">CvDicmeta</a></dd>
			<dd><a href="./cvDictionary/index" target="mainframe">CvDictionary</a></dd>
			<dd><a href="./doctor/index" target="mainframe">Doctor</a></dd>
			<dd><a href="./doctorSuggestion/index" target="mainframe">DoctorSuggestion</a></dd>
			<dd><a href="./evaluation/index" target="mainframe">Evaluation</a></dd>
			<dd><a href="./evaluationItem/index" target="mainframe">EvaluationItem</a></dd>
			<dd><a href="./evaluationOption/index" target="mainframe">EvaluationOption</a></dd>
			<dd><a href="./fieldControl/index" target="mainframe">FieldControl</a></dd>
			<dd><a href="./hl7NodeFieldConfig/index" target="mainframe">Hl7NodeFieldConfig</a></dd>
			<dd><a href="./hospital/index" target="mainframe">Hospital</a></dd>
			<dd><a href="./log/index" target="mainframe">Log</a></dd>
			<dd><a href="./menu/index" target="mainframe">Menu</a></dd>
			<dd><a href="./messageCache/index" target="mainframe">MessageCache</a></dd>
			<dd><a href="./messageNodeConfig/index" target="mainframe">MessageNodeConfig</a></dd>
			<dd><a href="./mqMessage/index" target="mainframe">MqMessage</a></dd>
			<dd><a href="./notice/index" target="mainframe">Notice</a></dd>
			<dd><a href="./patient/index" target="mainframe">Patient</a></dd>
			<dd><a href="./poll/index" target="mainframe">Poll</a></dd>
			<dd><a href="./pollItem/index" target="mainframe">PollItem</a></dd>
			<dd><a href="./receivedMessage/index" target="mainframe">ReceivedMessage</a></dd>
			<dd><a href="./role/index" target="mainframe">Role</a></dd>
			<dd><a href="./roleAccess/index" target="mainframe">RoleAccess</a></dd>
			<dd><a href="./setting/index" target="mainframe">Setting</a></dd>
			<dd><a href="./siteConfig/index" target="mainframe">SiteConfig</a></dd>
			<dd><a href="./suggestion/index" target="mainframe">Suggestion</a></dd>
			<dd><a href="./suggestionVer/index" target="mainframe">SuggestionVer</a></dd>
			<dd><a href="./sysControl/index" target="mainframe">SysControl</a></dd>
			<dd><a href="./task/index" target="mainframe">Task</a></dd>
			<dd><a href="./template/index" target="mainframe">Template</a></dd>
			<dd><a href="./tempmeta/index" target="mainframe">Tempmeta</a></dd>
			<dd><a href="./uaccess/index" target="mainframe">Uaccess</a></dd>
			<dd><a href="./users/index" target="mainframe">Users</a></dd>
			<dd><a href="./userRole/index" target="mainframe">UserRole</a></dd>
			<dd><a href="./workday/index" target="mainframe">Workday</a></dd>
	</dl>
</div>
<div class="main-frame">
	  <iframe frameborder="0"  name="mainframe"  id="mainframe" width="100%" height="100%"></iframe>
</div>
</body>
</html>
