from setuptools import setup, find_packages


def requirements():
    with open("requirements.txt") as f:
        return f.read().splitlines()


setup(
    name="BehaviourBranchAi",
    version="0.1",
    packages=find_packages(exclude=["Test"]),
)
