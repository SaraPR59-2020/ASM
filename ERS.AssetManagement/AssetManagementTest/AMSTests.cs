using AssetManagement;
using LocalControllerProject;
using LocalDeviceProject;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementTest
{
    [TestFixture]
    public class AMSTests
    {

        [Test]
        public void AMSConstructor() 
        {
            AMS ams = new AMS();

            Assert.IsNotNull(ams.MyConnection);
        }


        [Test]
        public void DaLiPostoji_ReturnsTrue() 
        {
            //Arrange
            AMS ams = new AMS();
            List<LocalDevice> devices = new List<LocalDevice>();
            LocalDevice localDevice = new LocalDevice("1", "1", 1, "1", 1, "1", 1);
            devices.Add(localDevice);
            //Act
            bool result = ams.DaLiPostoji(devices, localDevice);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DaLiPostoji_ReturnsFalse()
        {
            //Arrange
            AMS ams = new AMS();
            List<LocalDevice> devices = new List<LocalDevice>();
            LocalDevice localDevice = new LocalDevice("1", "1", 1, "1", 1, "1", 1);
            //Act
            bool result = ams.DaLiPostoji(devices, localDevice);

            //Assert
            Assert.IsFalse(result);
        }


        [Test]
       public void StartServerTest() 
       {
            //Arrange
            AMS ams = new AMS();


            //Act
            ams.StartServer();

            //Assert
            Assert.IsNotNull(ams.MyServer);
            Assert.IsNotNull(ams.MyStream);
        }

        [Test]
        public void ReceiveData_ReturnsTrue() 
        {
            //Arrange
            AMS ams = new AMS();

            var myTcp = new Mock<MyTcpListener>();
            myTcp.Setup(x => x.AcceptTcpClient()).Throws(new Exception());

            ams.MyServer = myTcp.Object;


            var stream = new Mock<MyNetworkStream>();
            stream.Setup(x => x.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            ams.MyStream = stream.Object;


            var db = new Mock<DataBase>();
            db.Setup(x => x.SaveData(It.IsAny<List<LocalDevice>>())).Verifiable();

            ams.DataBase = db.Object;
            //Act
            bool result = ams.ReceiveData();

            //Assert

            Assert.IsFalse(result);
        }
    }
}
